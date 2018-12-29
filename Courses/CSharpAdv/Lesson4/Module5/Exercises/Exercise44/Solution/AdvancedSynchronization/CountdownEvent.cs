using System.Threading;

namespace AdvancedSynchronization
{
    /// <summary>
    /// An event that becomes signaled after it is set for the specified number
    /// of times.  This resembles a "reverse" semaphore.
    /// </summary>
    class CountdownEvent
    {
        private int _count;
        private ManualResetEvent _done = new ManualResetEvent(false);

        public CountdownEvent(int count) { _count = count; }

        public void Set()
        {
            if (Interlocked.Decrement(ref _count) == 0)
                _done.Set();
        }

        public void Wait()
        {
            _done.WaitOne();
        }
    }
}
