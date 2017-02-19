using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Kafka.Consumer
{
    public interface IKafkaConsumerConnection
    {
        void Init();
    }

    public class KafkaConsumerConnection : IKafkaConsumerConnection, IDisposable
    {
        private readonly IKafkaConfiguration _config;
        private readonly ITopic _topic;
        private Confluent.Kafka.Consumer _kafkaConsumer;

        public KafkaConsumerConnection(IKafkaConfiguration config, ITopic topic)
        {
            _config = config;
            _topic = topic;
        }

        public void Init()
        {
            _kafkaConsumer = new Confluent.Kafka.Consumer(_config.ConsumerConfig);

            _kafkaConsumer.OnPartitionsAssigned += (_, partitions) => _kafkaConsumer.Assign(partitions);

            _kafkaConsumer.OnPartitionsRevoked += (_, partitions) => _kafkaConsumer.Unassign();

            _kafkaConsumer.Subscribe(_topic.Name);
        }

        public IEnumerable<string> Messages()
        {
            while (true)
            {
                if (_kafkaConsumer.Consume(out Message message, TimeSpan.FromMilliseconds(100)))
                {
                    if (message.Offset % 5 == 0)
                    {
                        _kafkaConsumer.CommitAsync(message);
                    }
                    yield return Encoding.UTF8.GetString(message.Value, 0, message.Value.Length);
                }
                else
                {
                    yield break;
                }
            }
        }

        public void Dispose()
        {
            _kafkaConsumer?.Dispose();
        }
    }
}