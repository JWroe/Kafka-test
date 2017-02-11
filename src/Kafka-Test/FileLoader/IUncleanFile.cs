using System.Collections.Generic;

namespace FileLoader
{
    internal interface IUncleanFile
    {
        IEnumerable<Record> Records();
    }
}