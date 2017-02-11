using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace Consumer
{
    class Program
    {
        static void Main()
        {
            using (var consumer = new Confluent.Kafka.Consumer(new Dictionary<string, object>
                                                               {
                                                                   { "group.id", "simple-produce-consume" },
                                                                   { "bootstrap.servers", "192.168.0.92:9092" },
                                                                   { "session.timeout.ms", 6000 },
                                                                   { "api.version.request", true }
                                                               }))
            {
                consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic: "test-topic", offset: Offset.Beginning, partition: 0) });

                Console.WriteLine("Messages Received:...");
                while (true)
                {
                    if (consumer.Consume(out Message msg, TimeSpan.FromSeconds(10)))
                    {
                        Console.WriteLine(Encoding.UTF8.GetString(msg.Value, 0, msg.Value.Length));
                    }
                }
            }
        }
    }
}