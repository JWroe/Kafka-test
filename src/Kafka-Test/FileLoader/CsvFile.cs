using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FileLoader
{
    internal class CsvFile : ICsvFile
    {
        private readonly IFileOnDisk _fileOnDisk;

        public CsvFile(IFileOnDisk fileOnDisk)
        {
            _fileOnDisk = fileOnDisk;
        }

        public IEnumerable<IEnumerable<string>> Content()
        {
            return _fileOnDisk.Lines().Select(line => line.Split(','));
        }
    }

    [TestFixture]
    internal class CsvFileTests
    {
        [Test]
        public void FileContentTest()
        {
            var fakeFileOnDisk = new IFileOnDisk_SampleCsvFile("this is line 1\n this is line 2\n this, is, split");
            var expected = new List<IEnumerable<string>>
                           {
                               new List<string> { "this is line 1" },
                               new List<string> { " this is line 2" },
                               new List<string> { " this", " is", " split" },
                           };
            var csvFile = new CsvFile(fakeFileOnDisk);

            Assert.That(csvFile.Content(), Is.EqualTo(expected));
        }
    }
}