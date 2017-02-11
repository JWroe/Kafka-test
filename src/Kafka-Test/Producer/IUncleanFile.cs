using System.Collections.Generic;

namespace Producer
{
    internal interface IUncleanFile
    {
        IEnumerable<string> UncleanRecords();
    }
}