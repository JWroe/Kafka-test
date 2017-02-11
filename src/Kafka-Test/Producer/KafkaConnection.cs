using Confluent.Kafka;

namespace Producer
{
    public class KafkaConnection : IKafkaConnection
    {
        private readonly IKafkaConfiguration _config;

        public KafkaConnection(IKafkaConfiguration config)
        {
            _config = config;
        }

        private Producer<Null, byte[]> _kafka;

        public void Init()
        {
            _kafka = new Producer<Null, byte[]>(_config.ConfigDictionary, null, new NonSerializer());
        }

        public void PublishMessage(ITopic topic, ISerializedObject objectToSend)
        {
            _kafka.ProduceAsync(topic.Name, null, objectToSend.Value);
        }
    }
}