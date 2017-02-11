using Newtonsoft.Json;

namespace KafkaConnection
{
    public interface IDeserializedObject<out T> where T : class
    {
        T Value { get; }
    }

    internal class DeserializedObject<T> : IDeserializedObject<T> where T : class
    {
        private readonly string _jsonSerializedObject;
        private T _value;

        public DeserializedObject(string jsonSerializedObject)
        {
            _jsonSerializedObject = jsonSerializedObject;
        }

        public T Value => _value ?? (_value = JsonConvert.DeserializeObject<T>(_jsonSerializedObject));
    }
}