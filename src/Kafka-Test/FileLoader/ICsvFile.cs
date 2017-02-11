using System.Collections.Generic;

namespace FileLoader
{
    internal interface ICsvFile
    {
        IEnumerable<IEnumerable<string>> Content();
    }
}