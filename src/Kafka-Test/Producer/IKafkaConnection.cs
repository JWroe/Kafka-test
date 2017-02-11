namespace Producer
{
    public interface IKafkaConnection
    {
        void Init();
        void PublishMessage(ITopic topic, ISerializedObject objectToSend);
    }
}