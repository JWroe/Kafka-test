using System.Collections.Generic;

namespace FileLoader
{
    internal interface ICsvFile
    {
        IEnumerable<IEnumerable<string>> Content();
    }

    internal class ICsvFile_FakeCsvFile : ICsvFile
    {
        private readonly IEnumerable<IEnumerable<string>> _content;

        public ICsvFile_FakeCsvFile(IEnumerable<IEnumerable<string>> content)
        {
            _content = content;
        }

        public IEnumerable<IEnumerable<string>> Content() => _content;
    }
}