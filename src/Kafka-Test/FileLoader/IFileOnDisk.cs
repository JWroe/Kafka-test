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
        private readonly string _content;

        public IFileOnDisk_SampleCsvFile(string content)
        {
            _content = content;
        }

        public string Content() => _content;

        public IEnumerable<string> Lines() => _content.Split('\n');
    }
}