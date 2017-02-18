using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using NUnit.Framework;

namespace KafkaConnection.Producer
{
    public interface IKafkaProducerConnection
    {
        void Init();
        Task PublishMessageAsync(ITopic topic, string objectToSend);
    }

    public class KafkaProducerConnection : IKafkaProducerConnection, IDisposable
    {
        private readonly IKafkaConfiguration _config;

        public KafkaProducerConnection(IKafkaConfiguration config)
        {
            _config = config;
        }

        private Producer<Null, string> _kafka;

        public void Init()
        {
            _kafka = new Producer<Null, string>(_config.ProducerConfig, null, new StringSerializer(Encoding.UTF8));
        }

        public Task PublishMessageAsync(ITopic topic, string objectToSend)
        {
            return _kafka.ProduceAsync(topic.Name, null, objectToSend);
        }

        public void Flush()
        {
            _kafka.Flush();
        }

        public void Dispose()
        {
            _kafka.Flush();
            _kafka.Dispose();
        }
    }

    [TestFixture]
    public class KafkaConnectionTests
    {
        [Test]
        public void SimpleProduceConsume()
        {
            const string testString = "hello world";
            const string kafkaServer = "192.168.0.91";

            var testTopic = Guid.NewGuid().ToString();

            var connection = new KafkaProducerConnection(new KafkaConfiguration(kafkaServer));
            connection.Init();
            connection.PublishMessageAsync(new Topic(testTopic), testString).Wait();

            var consumerConfig = new Dictionary<string, object>
                                 {
                                     { "group.id", "simple-produce-consume" },
                                     { "bootstrap.servers", kafkaServer },
                                     { "session.timeout.ms", 6000 },
                                     { "api.version.request", true }
                                 };

            using (var consumer = new Confluent.Kafka.Consumer(consumerConfig))
            {
                consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(testTopic, partition: 0, offset: Offset.Beginning) });

                var watch = Stopwatch.StartNew();
                while (true)
                {
                    if (watch.Elapsed >= TimeSpan.FromSeconds(10))
                    {
                        Assert.Fail();
                    }
                    if (consumer.Consume(out Message message, TimeSpan.FromSeconds(10)))
                    {
                        Assert.That(testString, Is.EqualTo(Encoding.UTF8.GetString(message.Value, 0, message.Value.Length)));
                        Assert.That(null, Is.EqualTo(message.Key));
                        break;
                    }
                }
            }
        }
    }
}