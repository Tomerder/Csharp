using System;
using System.Diagnostics;

namespace ParallelAndConcurrent
{
    /// <summary>
    /// Demonstrates how to use data parallelism and concurrent collections.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            const int Begin = 2;
            const int End = 2000002;

            Measure("Serial Primes", () => Primes.AllPrimesSerial(Begin, End));
            Measure("Parallel Primes", () => Primes.AllPrimesParallel(Begin, End));

            MeasureAndCountThreads("Parallel Primes", () => Primes.AllPrimesParallelAndCountThreads(Begin, End));
        }

        static void Measure(string what, Action act)
        {
            Console.WriteLine("Measuring {0}...", what);
            Stopwatch sw = Stopwatch.StartNew();
            act();
            Console.WriteLine("{0} ms", sw.ElapsedMilliseconds);
        }

        static void MeasureAndCountThreads(string what, Func<int> func)
        {
            Console.WriteLine("Measuring {0}...", what);
            Stopwatch sw = Stopwatch.StartNew();
            int i = func();
            Console.WriteLine("{0} ms : {1}", sw.ElapsedMilliseconds, i);
        }
    }
}
