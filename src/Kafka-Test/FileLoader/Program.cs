using NullGuard;

[assembly: NullGuard(ValidationFlags.All)]

namespace FileLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //var service = new FileProcessorService(new KafkaConfiguration(ConfigurationManager.AppSettings["KafkaServer"]));
            //service.Init();

            //Console.WriteLine("Enter the path of your csv file to process...");
            //var path = Console.ReadLine();

            //if (File.Exists(path))
            //{
            //    service.RegisterFileForCleaning(path);
            //}
        }
    }
}