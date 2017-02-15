using System;
using System.Configuration;
using System.IO;
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
            var producerConnection = new KafkaProducerConnection(config);
            var topic = new Topic("my-topic");
            var service = new FileProcessorService(producerConnection, topic);
            service.Init();

            Console.WriteLine($"topic - {topic.Name}");
            while (true)
            {
                Console.WriteLine("Enter the number of messages to publish...");
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    for (var i = 0; i < num; i++)
                    {
                        producerConnection.PublishMessageAsync(topic, $"Message #{i}");
                    }
                }
            }
        }
    }
}