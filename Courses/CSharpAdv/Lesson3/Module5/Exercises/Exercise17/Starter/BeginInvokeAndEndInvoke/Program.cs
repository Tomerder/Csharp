using System;
using System.Threading;

namespace APM
{
    /// <summary>
    /// Demonstrates the use of the APM with arbitrary delegates for performing
    /// a prime number calculation.  The code performs a synchronous calculation first,
    /// and then performs asynchronously calcualtions
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Synchronous calculation:
            PrimeNumberCalculator calc = new PrimeNumberCalculator(5, 5000000);
            Measure.It(() =>
            {
                Console.WriteLine("There are {0} primes", calc.Calculate());
                Console.WriteLine("There are {0} primes", calc.Calculate());
            }, "Synchronous calculation");

            //Asynchronous calculation using delegates to perform BOTH calculations at the same time:
            PrimeNumberCalculator primeNumberCalculator = new PrimeNumberCalculator(5, 5000000);
            Measure.It(delegate
            {
                // TODO

                //Polling the IsCompleted property of both async results every 100ms:
                while (!(iar1.IsCompleted && iar2.IsCompleted))
                    Thread.Sleep(100);

                Console.WriteLine("There are {0} primes", asyncCalculate1.EndInvoke(iar1));
                Console.WriteLine("There are {0} primes", asyncCalculate2.EndInvoke(iar2));

            }, "Asynchronous calculation");
        }
    }
}
