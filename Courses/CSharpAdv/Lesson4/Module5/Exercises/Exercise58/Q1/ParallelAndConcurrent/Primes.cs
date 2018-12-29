using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelAndConcurrent
{
    /// <summary>
    /// Demonstrates the use of Parallel.For and ConcurrentBag to parallelize prime
    /// number computation and obtain all the prime numbers in a given range.
    /// </summary>
    class Primes
    {
        /// <summary>
        /// Checks whether a number is prime.
        /// </summary>
        private static bool IsPrime(int n)
        {
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            int root = (int)Math.Sqrt(n);
            for (int x = 3; x <= root; x += 2)
                if (n % x == 0) return false;
            return true;
        }

        /// <summary>
        /// Returns all the prime numbers in the range (begin, end] using a single thread
        /// for computations.
        /// </summary>
        public static IEnumerable<int> AllPrimesSerial(int begin, int end)
        {
            List<int> primes = new List<int>();
            for (int n = begin; n < end; ++n)
            {
                if (IsPrime(n)) primes.Add(n);
            }
            return primes;
        }

        /// <summary>
        /// Returns all the prime numbers in the range (begin, end] using multiple threads,
        /// as determined by the Parallel.For API. Stores the numbers in a ConcurrentBag
        /// to avoid the need for explicit synchronization when accessing the shared collection.
        /// </summary>
        public static IEnumerable<int> AllPrimesParallel(int begin, int end)
        {
            List<int> dic = new List<int>();

            ConcurrentBag<int> primes = new ConcurrentBag<int>();
            Parallel.For(begin, end, n =>
            {
                if (IsPrime(n)) primes.Add(n);
                if (!dic.Contains(Thread.CurrentThread.ManagedThreadId))
                {
                    dic.Add(Thread.CurrentThread.ManagedThreadId);
                }
            });
            dic.ForEach(d => Console.WriteLine(d));
            return primes;
        }
    }
}
