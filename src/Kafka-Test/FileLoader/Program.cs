using System;
using System.Configuration;
using System.IO;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using KafkaConnection;
using KafkaConnection.Producer;
using NullGuard;

[assembly: NullGuard(ValidationFlags.All)]

namespace FileLoader
{
    class Program
    {
        static void Main()
        {
            var config = new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]);
            using (var producerConnection = new Producer<Null, string>(config.ProducerConfig, null, new StringSerializer(Encoding.UTF8)))
            {
                while (true)
                {
                    Console.WriteLine("Enter the number of messages to publish...");
                    if (int.TryParse(Console.ReadLine(), out int num))
                    {
                        for (var i = 0; i < num; i++)
                        {
                            producerConnection.ProduceAsync("my-topic", null, $"Message #{i}");
                        }
                        producerConnection.Flush();
                    }
                }
            }
        }
    }
}