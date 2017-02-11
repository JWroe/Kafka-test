using System;
using FileLoader;

namespace IFileOnDisk
{
    internal interface IUncleanFile
    {
    }

    internal class UncleanFile : IUncleanFile
    {
        public UncleanFile(ICsvFile csvFile)
        {
            throw new NotImplementedException();
        }
    }
}