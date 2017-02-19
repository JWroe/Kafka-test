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

        public TimeMetric LastFileMetrics { get; set; }

        public void Init()
        {
            _kafkaProducerConnection.Init();
        }

        public void RegisterFileForCleaning(IFileOnDisk fileToProcess)
        {
            LastFileMetrics = new TimeMetric().RunningMetric();
            foreach (var line in fileToProcess.Lines())
            {
                _kafkaProducerConnection.PublishMessageAsync(_topic, line);
                LastFileMetrics = LastFileMetrics.Increment();
            }
            _kafkaProducerConnection.Flush();
            LastFileMetrics = LastFileMetrics.StoppedMetric();
        }
    }
}