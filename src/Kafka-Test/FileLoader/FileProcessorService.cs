using KafkaConnection;
using KafkaConnection.Producer;

namespace FileLoader
{
    internal class FileProcessorService
    {
        private readonly Topic _uncleanedRecordsTopic = new Topic("UncleanedRecords");
        private readonly IKafkaProducerConnection _kafkaProducerConnection;

        public FileProcessorService(IKafkaProducerConnection kafkaProducerConnection)
        {
            _kafkaProducerConnection = kafkaProducerConnection;
        }

        public void Init()
        {
            _kafkaProducerConnection.Init();
        }

        public void RegisterFileForCleaning(IFileOnDisk fileToProcess)
        {
            foreach (var record in fileToProcess.Lines())
            {
                _kafkaProducerConnection.PublishMessage(_uncleanedRecordsTopic, record);
            }
        }
    }
}
