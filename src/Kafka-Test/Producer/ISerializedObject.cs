using Newtonsoft.Json;

namespace Producer
{
    public interface IJsonSerializedObject
    {
        string Value { get; }
    }

    public class JsonSerializedObject : IJsonSerializedObject
    {
        private readonly object _object;
        private string _value;

        public JsonSerializedObject(object obj)
        {
            _object = obj;
        }

        public string Value => _value ?? (_value = JsonConvert.SerializeObject(_object));
    }
}