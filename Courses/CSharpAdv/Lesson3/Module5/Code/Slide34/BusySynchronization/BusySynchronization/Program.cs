using System;
using System.Threading;

namespace BusySynchronization
{
    /// <summary>
    /// Demonstrates how interlocked synchronization is used to ensure that a 
    /// counter variable is incremented atomically.
    /// </summary>
    class Program
    {
        const int INCREMENTS = 10000;
        const int PARALLELISM = 10;

        static void Main(string[] args)
        {
            InterlockedCounter globalCounter = new InterlockedCounter();

            for (int i = 0; i < PARALLELISM; ++i)
            {
                ThreadPool.QueueUserWorkItem(IncrementInterlockedCounterLotsOfTimes, globalCounter);
            }

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Expected: {0}, actual: {1}", PARALLELISM * INCREMENTS, globalCounter.Current);
        }

        private static void IncrementInterlockedCounterLotsOfTimes(object obj)
        {
            InterlockedCounter counter = (InterlockedCounter)obj;
            for (int i = 0; i < INCREMENTS; ++i)
                counter.Next();
            Console.WriteLine("Completed");
        }
    }
}
