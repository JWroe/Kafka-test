using NUnit.Framework;
using Producer;

namespace FileLoader
{
    internal class FileProcessorService
    {
        private readonly IKafkaConnection _kafkaConnection;

        public FileProcessorService(IKafkaConnection kafkaConnection)
        {
            _kafkaConnection = kafkaConnection;
        }

        public void Init()
        {
            _kafkaConnection.Init();
        }

        public void RegisterFileForCleaning(IUncleanFile fileToProcess)
        {
        }
    }

    internal class FileProcessingServiceTests
    {
        [Test]
        public void RegisterFileForCleaningTest()
        {
        }
    }
}