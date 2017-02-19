using System;
using System.Configuration;
using Kafka;
using Kafka.Producer;

namespace FileLoader
{
    class Program
    {
        static void Main()
        {
            var topic = new Topic("my-topic");
            var config = new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]);
            using (var producerConnection = new KafkaProducerConnection(config))
            {
                producerConnection.Init();
                while (true)
                {
                    Console.WriteLine("Enter the number of messages to publish...");
                    if (int.TryParse(Console.ReadLine(), out int num))
                    {
                        for (var i = 0; i < num; i++)
                        {
                            producerConnection.PublishMessageAsync(topic, "Test");
                        }
                        producerConnection.Flush();
                    }
                }
            }
        }
    }
}