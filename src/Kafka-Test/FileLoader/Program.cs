using System;
using System.Configuration;
using System.IO;
using Kafka;
using Kafka.Producer;

namespace FileLoader
{
    class Program
    {
        static void Main()
        {
            var topic = new Topic("raw-records");
            var config = new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]);
            using (var producerConnection = new KafkaProducerConnection(config))
            {
                var service = new FileProcessorService(producerConnection, topic);
                service.Init();

                while (true)
                {
                    Console.WriteLine("Enter the location of a file to parse...");
                    var path = Console.ReadLine();
                    if (File.Exists(path))
                    {
                        var fileOnDisk = new FileOnDisk(path);
                        service.RegisterFileForCleaning(fileOnDisk);
                        Console.WriteLine($"{service.LastFileMetrics.Count} records produced in {service.LastFileMetrics.TimeTaken}, {service.LastFileMetrics.Count / service.LastFileMetrics.TimeTaken.TotalSeconds} records per second");
                    }
                    else
                    {
                        Console.WriteLine("File not found!");
                    }
                }
            }
        }
    }
}