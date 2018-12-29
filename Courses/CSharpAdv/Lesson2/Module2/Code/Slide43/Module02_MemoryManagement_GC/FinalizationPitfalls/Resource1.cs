using System.Threading;

namespace FinalizationPitfalls
{
    /// <summary>
    /// Demonstrates the memory leak scenario.
    /// </summary>
    class Resource1
    {
        private byte[] _data = new byte[16384];

        public Resource1()
        {
            Thread.Sleep(10);
        }

        ~Resource1()
        {
            Thread.Sleep(20);
        }
    }
}
