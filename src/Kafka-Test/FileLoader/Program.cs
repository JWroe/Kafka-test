using System;

namespace FileLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter the path of your file to process...");
                var path = Console.ReadLine();
                //var uncleanFile = new UncleanFile(new CsvFile(new FileOnDisk(path)));

                //var kafkaConnection = new KafkaConnection(new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]));
            }
        }
    }
}