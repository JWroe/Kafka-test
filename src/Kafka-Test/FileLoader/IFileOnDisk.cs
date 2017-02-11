using System.Collections.Generic;

namespace FileLoader
{
    internal interface IFileOnDisk
    {
        string Content();
        IEnumerable<string> Lines();
    }

    internal class IFileOnDisk_SampleCsvFile : IFileOnDisk
    {
        public string Content()
        {
            return "this is line 1\n this is line 2\n this, is, split";
        }

        public IEnumerable<IEnumerable<string>> Csv() => new List<IEnumerable<string>>
                                                         {
                                                             new List<string> { "this is line 1" },
                                                             new List<string> { " this is line 2" },
                                                             new List<string> { " this", " is", " split" },
                                                         };

        public IEnumerable<string> Lines()
        {
            return new List<string>
                   {
                       "this is line 1",
                       " this is line 2",
                       " this, is, split"
                   };
        }
    }
}