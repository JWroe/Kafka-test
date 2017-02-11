using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Producer
{
    class Program
    {
        static void Main()
        {
            using (var producer = new Producer<Null, string>(new Dictionary<string, object>
                                                             {
                                                                 { "bootstrap.servers", "192.168.0.92:9092" },
                                                                 { "api.version.request", true }
                                                             }, null, new StringSerializer(Encoding.UTF8)))
            {
                while (true)
                {
                    Console.WriteLine("Enter your message to send");
                    var input = Console.ReadLine();
                    producer.ProduceAsync("test-topic", null, input);
                    producer.Flush();
                }
            }
        }
    }
}