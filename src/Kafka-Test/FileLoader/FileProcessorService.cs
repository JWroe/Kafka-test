using Producer;

namespace FileLoader
{
    internal class FileProcessorService
    {
        private readonly Topic _uncleanedRecordsTopic = new Topic("UncleanedRecords");
        private readonly IKafkaConnection _kafkaConnection;

        public FileProcessorService(IKafkaConnection kafkaConnection)
        {
            _kafkaConnection = kafkaConnection;
        }

        public void Init()
        {
            _kafkaConnection.Init();
        }

        public void RegisterFileForCleaning(IFileOnDisk fileToProcess)
        {
            foreach (var record in fileToProcess.Lines())
            {
                _kafkaConnection.PublishMessage(_uncleanedRecordsTopic, record);
            }
        }
    }
}