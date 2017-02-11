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
            var service = new FileProcessorService(producerConnection);
            service.Init();

            while (true)
            {
                Console.WriteLine("Enter the path of your csv file to process...");
                var path = Console.ReadLine();

                if (File.Exists(path))
                {
                    var file = new FileOnDisk(path);
                    service.RegisterFileForCleaning(file);
                }
            }
        }
    }
}