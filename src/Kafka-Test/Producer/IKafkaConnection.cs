using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using NUnit.Framework;

namespace Producer
{
    public interface IKafkaConnection
    {
        void Init();
        void PublishMessage(ITopic topic, ISerializedObject objectToSend);
    }

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

        private class NonSerializer : ISerializer<byte[]>
        {
            public byte[] Serialize(byte[] data)
            {
                return data;
            }
        }
    }

    [TestFixture]
    public class KafkaConnectionTests
    {
        [Test]
        public void SimpleProduceConsume()
        {
            const string testString = "hello world";
            const string testTopic = "testtopic";
            const string kafkaServer = "192.168.0.92";

            var connection = new KafkaConnection(new KafkaConfiguration(kafkaServer));
            connection.Init();
            connection.PublishMessage(new Topic(testTopic), new SerializedString(testString));

            var consumerConfig = new Dictionary<string, object>
                                 {
                                     { "group.id", "simple-produce-consume" },
                                     { "bootstrap.servers", kafkaServer },
                                     { "session.timeout.ms", 6000 },
                                     { "api.version.request", true }
                                 };

            using (var consumer = new Consumer(consumerConfig))
            {
                consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(testTopic, partition: 0, offset: Offset.Beginning) });
                Assert.That(consumer.Consume(out Message message, TimeSpan.FromSeconds(10)), Is.True);
                Assert.That(testString, Is.EqualTo(Encoding.UTF8.GetString(message.Value, 0, message.Value.Length)));
                Assert.That(null, Is.EqualTo(message.Key));
            }
        }
    }
}