using System.Threading;

namespace BusySynchronization
{
    /// <summary>
    /// A counter that is protected with interlocked operations
    /// to make sure its value is atomically incremented.
    /// </summary>
    class InterlockedCounter
    {
        private int _value;

        public int Next()
        {
            return Interlocked.Increment(ref _value);
        }

        public int Current { get { return _value; } }
    }
}
