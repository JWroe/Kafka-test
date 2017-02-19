using System;
using System.Configuration;
using System.IO;
using System.Threading;
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
                producerConnection.Init();
                while (true)
                {
                    Console.WriteLine("Enter the location of a file to parse...");
                    var path = Console.ReadLine();
                    var metric = new TimeMetric();
                    if (File.Exists(path))
                    {
                        metric = metric.RunningMetric();
                        var file = new FileOnDisk(path);
                        foreach (var line in file.Lines())
                        {
                            producerConnection.PublishMessageAsync(topic, line);
                            metric = metric.Increment();
                        }
                        producerConnection.Flush();
                        metric = metric.StoppedMetric();
                        Console.WriteLine($"{metric.Count} records produced in {metric.TimeTaken}");
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