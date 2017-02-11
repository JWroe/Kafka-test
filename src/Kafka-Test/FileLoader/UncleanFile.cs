using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FileLoader
{
    internal class UncleanFile : IUncleanFile
    {
        private readonly ICsvFile _csvFile;

        public UncleanFile(ICsvFile csvFile)
        {
            _csvFile = csvFile;
        }

        public IEnumerable<Record> Records()
        {
            return _csvFile.Content().Select(line => new Record(line));
        }
    }

    internal class UncleanFileTests
    {
        [Test]
        public void Test()
        {
            var dataIn = new List<List<string>>
                         {
                             new List<string> { "1" },
                             new List<string> { "2", "3" },
                             new List<string> { "3", "2", "44" },
                             new List<string> { "4" },
                             new List<string> { "5" }
                         };

            var expected = new List<Record>
                           {
                               new Record(new List<string> { "1" }),
                               new Record(new List<string> { "2", "3" }),
                               new Record(new List<string> { "3", "2", "44" }),
                               new Record(new List<string> { "4" }),
                               new Record(new List<string> { "5" })
                           };

            var fakeCsv = new ICsvFile_FakeCsvFile(dataIn);

            var uncleanFile = new UncleanFile(fakeCsv);

            Assert.That(uncleanFile.Records(), Is.EquivalentTo(expected));
        }
    }
}