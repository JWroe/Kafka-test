using System;
using System.Linq;
using Kafka;
using Kafka.Producer;

namespace FileLoader
{
    internal class FileProcessorService
    {
        private readonly IKafkaProducerConnection _kafkaProducerConnection;
        private readonly Topic _topic;

        public FileProcessorService(IKafkaProducerConnection kafkaProducerConnection, Topic topic)
        {
            _kafkaProducerConnection = kafkaProducerConnection;
            _topic = topic;
        }

        public void Init()
        {
            _kafkaProducerConnection.Init();
        }

        public void RegisterFileForCleaning(IFileOnDisk fileToProcess)
        {
            var i = 0;
            foreach (var line in fileToProcess.Lines())
            {
                i++;
                if (i % 100 == 0)
                {
                    Console.WriteLine(i);
                }
                
            }
        }
    }
}