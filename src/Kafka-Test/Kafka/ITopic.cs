namespace Kafka
{
    public interface ITopic
    {
        string Name { get; }
    }

    public class Topic : ITopic
    {
        public Topic(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}