using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Kafka;
using Kafka.Consumer;
using Kafka.Producer;

namespace RecordChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new HashSet<int>();
            Populate(db);

            var config = new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]);

            var unprocessed = new Topic("raw-records");
            using (var consumer = new KafkaConsumerConnection(config, unprocessed))
            {
                consumer.Init();
                var match = new Topic("removed-records");
                var noMatch = new Topic("allowed-records");
                using (var producer = new KafkaProducerConnection(config))
                {
                    Console.WriteLine("Waiting for messages");

                    producer.Init();
                    var metric = new TimeMetric().RunningMetric();
                    while (true)
                    {
                        foreach (var message in consumer.Messages())
                        {
                            metric = metric.Increment();
                            if (metric.Count % 10000 == 0)
                            {
                                Console.WriteLine($"{metric.Count} records processed in {metric.TimeTaken} - {metric.Count / metric.TimeTaken.TotalSeconds} records per second");
                            }

                            var id = message.Split(',')[0];
                            if (int.TryParse(id, out int i) && db.Contains(i))
                            {
                                producer.PublishMessageAsync(match, message);
                            }
                            else
                            {
                                producer.PublishMessageAsync(noMatch, message);
                            }
                        }
                    }
                }
            }
        }

        private static void Populate(ISet<int> db)
        {
            foreach (var item in Enumerable.Range(1, 100000).Where(i => i % 4 == 0))
            {
                db.Add(item);
            }
        }
    }
}