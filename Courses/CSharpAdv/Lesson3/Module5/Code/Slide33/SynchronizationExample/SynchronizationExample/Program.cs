using System;
using System.Threading;

namespace SynchronizationExample
{
    /// <summary>
    /// Shows that it is possible for a shared variable that is modified without
    /// synchronization to become corrupted.  In this case, a counter is incremented
    /// a certain number of times in parallel, but the resulting value is not
    /// necessarily what we expect.
    /// </summary>
    class Program
    {
        const int INCREMENTS = 10000;
        const int PARALLELISM = 10;

        static void Main(string[] args)
        {
            Counter globalCounter = new Counter();

            for (int i = 0; i < PARALLELISM; ++i)
            {
                ThreadPool.QueueUserWorkItem(IncrementCounterLotsOfTimes, globalCounter);
            }

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Expected: {0}, actual: {1}", PARALLELISM * INCREMENTS, globalCounter.Current);
        }

        private static void IncrementCounterLotsOfTimes(object obj)
        {
            Counter counter = (Counter)obj;
            for (int i = 0; i < INCREMENTS; ++i)
                counter.Next();
            Console.WriteLine("Completed");
        }
    }
}
