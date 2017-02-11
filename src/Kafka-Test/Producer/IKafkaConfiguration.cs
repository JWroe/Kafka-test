using System.Collections.Generic;

namespace Producer
{
    public interface IKafkaConfiguration
    {
        Dictionary<string, object> ConfigDictionary { get; }
    }

    public class KafkaConfiguration : IKafkaConfiguration
    {
        private readonly string _server;

        public KafkaConfiguration(string server)
        {
            _server = server;
        }

        public Dictionary<string, object> ConfigDictionary => new Dictionary<string, object>
                                                              {
                                                                  { "bootstrap.servers", _server },
                                                                  { "api.version.request", true }
                                                              };
    }
}