using System.Collections.Generic;

namespace Kafka
{
    public interface IKafkaConfiguration
    {
        Dictionary<string, object> ProducerConfig { get; }
        Dictionary<string, object> ConsumerConfig { get; }
    }

    public class KafkaConfiguration : IKafkaConfiguration
    {
        private readonly string _server;

        public KafkaConfiguration(string server)
        {
            _server = server;
        }

        public Dictionary<string, object> ProducerConfig => new Dictionary<string, object>
                                                            {
                                                                { "bootstrap.servers", _server },
                                                                { "api.version.request", true }
                                                            };

        public Dictionary<string, object> ConsumerConfig => new Dictionary<string, object>
                                                            {
                                                                { "group.id", "simple-produce-consume" },
                                                                { "bootstrap.servers", _server },
                                                                { "session.timeout.ms", 6000 },
                                                                { "api.version.request", true },
                                                                { "auto.commit.interval.ms", 1000 },
                                                                { "enable.auto.commit", true }
                                                            };
    }
}