using System;
using System.Configuration;
using System.IO;
using NullGuard;
using Producer;

[assembly: NullGuard(ValidationFlags.All)]

namespace FileLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var kafkaConnection = new KafkaConnection(new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]));
            var service = new FileProcessorService(kafkaConnection);
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