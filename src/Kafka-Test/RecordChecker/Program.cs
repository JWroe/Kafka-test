using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using KafkaConnection;
using KafkaConnection.Consumer;
using NullGuard;

[assembly: NullGuard(ValidationFlags.All)]

namespace RecordChecker
{
    class Program
    {
        private static HashSet<string> _disallowedInputs;

        static void Main()
        {
            try
            {
                InitRepository();

                var config = new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]);
                var consumerConnection = new KafkaConsumerConnection(config);
                consumerConnection.Init();

                Console.WriteLine("Ready for jobs");
                var allowed = 0;
                var stopped = 0;
                var watch = new Stopwatch();
                foreach (var message in consumerConnection.Messages(new Topic("UncleanedRecords")))
                {
                    if (!watch.IsRunning)
                    {
                        watch.Start();
                    }

                    var id = message.Split(',')[0];
                    if (_disallowedInputs.Contains(id))
                    {
                        stopped++;
                    }
                    else
                    {
                        allowed++;
                    }
                }
                Console.WriteLine($"{allowed + stopped} records processed in {watch.Elapsed.TotalSeconds} - allowed {allowed} and stopped {stopped}");

                Console.WriteLine("done");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void InitRepository()
        {
            _disallowedInputs = new HashSet<string>();

            foreach (var j in Enumerable.Range(0, 1000000).Where(i => i % 3 == 0))
            {
                _disallowedInputs.Add(j.ToString());
            }
        }
    }
}