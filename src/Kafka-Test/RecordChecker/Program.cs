using System;
using System.Configuration;
using KafkaConnection;
using KafkaConnection.Consumer;
using NullGuard;

[assembly: NullGuard(ValidationFlags.All)]

namespace RecordChecker
{
    class Program
    {
        static void Main()
        {
            var config = new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]);
            var consumerConnection = new KafkaConsumerConnection(config);
            consumerConnection.Init();
            foreach (var message in consumerConnection.GetMessages(new Topic("UncleanedRecords")))
            {
                Console.WriteLine(message);
            }

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}