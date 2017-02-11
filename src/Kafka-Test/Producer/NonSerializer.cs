using Confluent.Kafka.Serialization;

namespace Producer
{
    internal class NonSerializer : ISerializer<byte[]>
    {
        public byte[] Serialize(byte[] data)
        {
            return data;
        }
    }
}