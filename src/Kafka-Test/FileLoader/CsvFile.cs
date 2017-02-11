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
            var fakeFileOnDisk = new IFileOnDisk_SampleCsvFile();

            var csvFile = new CsvFile(fakeFileOnDisk);

            Assert.That(csvFile.Content(), Is.EqualTo(fakeFileOnDisk.Csv()));
        }
    }
}