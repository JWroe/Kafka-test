using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace KafkaConnection.Consumer
{
    public interface IKafkaConsumerConnection
    {
        void Init();
    }

    public class KafkaConsumerConnection : IKafkaConsumerConnection
    {
        private readonly IKafkaConfiguration _config;
        private Confluent.Kafka.Consumer _kafkaConsumer;

        public KafkaConsumerConnection(IKafkaConfiguration config)
        {
            _config = config;
        }

        public void Init()
        {
            _kafkaConsumer = new Confluent.Kafka.Consumer(_config.ConsumerConfig);
        }

        public IEnumerable<string> GetMessages(ITopic topic)
        {
            _kafkaConsumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic.Name, partition: 0, offset: Offset.Beginning) });

            while (true)
            {
                if (_kafkaConsumer.Consume(out Message message, TimeSpan.FromSeconds(10)))
                {
                    yield return Encoding.UTF8.GetString(message.Value, 0, message.Value.Length);
                }
            }
        }
    }
}