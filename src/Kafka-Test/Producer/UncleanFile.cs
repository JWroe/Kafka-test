using System.Collections.Generic;

namespace Producer
{
    internal class UncleanFile : IUncleanFile
    {
        public IEnumerable<string> UncleanRecords()
        {
            for (var i = 0; i < 10000; i++)
            {
                yield return "";
            }
        }
    }
}