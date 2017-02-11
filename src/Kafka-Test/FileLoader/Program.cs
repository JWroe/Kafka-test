using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var uncleanFile = new UncleanFile(new FileOnDisk(path));
            }

        }
    }

    internal class FileOnDisk
    {
        private readonly string _path;

        public FileOnDisk(string path)
        {
            _path = path;
            
        }
    }
}