using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace FileLoader
{
    internal class FileOnDisk : IFileOnDisk
    {
        private readonly string _path;

        public FileOnDisk(string path)
        {
            _path = path;
        }

        public string Content()
        {
            return File.ReadAllText(_path);
        }

        public IEnumerable<string> Lines()
        {
            var i = 0;
            while (true)
                yield return $"{i++},{Guid.NewGuid()},Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat";
        }
    }

    [TestFixture]
    internal class FileOnDiskTests
    {
        [Test]
        public void FileContentTest()
        {
            var path = Path.GetTempFileName();
            var data = Guid.NewGuid().ToString();
            File.WriteAllText(path, data);

            var fileOnDisk = new FileOnDisk(path);
            Assert.That(fileOnDisk.Content(), Is.EqualTo(data));
        }

        [Test]
        public void FileGetLinesTest()
        {
            var path = Path.GetTempFileName();
            var guid1 = Guid.NewGuid().ToString();
            var guid2 = Guid.NewGuid().ToString();
            var data = $"{guid1}\n{guid2}";
            File.WriteAllText(path, data);

            var fileOnDisk = new FileOnDisk(path);
            Assert.That(fileOnDisk.Lines().Count(), Is.EqualTo(2));
            Assert.That(fileOnDisk.Lines().ElementAt(0), Is.EqualTo(guid1));
            Assert.That(fileOnDisk.Lines().ElementAt(1), Is.EqualTo(guid2));
        }
    }
}