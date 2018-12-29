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
            PrimeNumberCalculator calc = new PrimeNumberCalculator(0, 10000000);
            Measure.It(() =>
            {
                Console.WriteLine("There are {0} primes", calc.Calculate());
            }, "Synchronous calculation");

            //Asynchronous calculation using delegates to perform BOTH calculations at the same time:
            PrimeNumberCalculator primeNumberCalculator1 = new PrimeNumberCalculator(0, 5000000);
            PrimeNumberCalculator primeNumberCalculator2 = new PrimeNumberCalculator(5000001, 10000000);

            Measure.It(
                    delegate   //first parameter of Measure.It => Action
                    {
                        // TODO
                        Func<int> del1 = primeNumberCalculator1.Calculate;
                        Func<int> del2 = primeNumberCalculator2.Calculate;

                        IAsyncResult iar1 = del1.BeginInvoke(null, null);
                        IAsyncResult iar2 = del2.BeginInvoke(null, null);

                        //Polling the IsCompleted property of both async results every 100ms:
                        //while (!(iar1.IsCompleted && iar2.IsCompleted))
                        //    Thread.Sleep(100);

                        int sum1 = del1.EndInvoke(iar1);
                        int sum2 = del2.EndInvoke(iar2);
                        Console.WriteLine("There are {0} primes", sum1 + sum2);
                    }
                    , "Asynchronous calculation"); //second parameter of Measure.It

            Console.ReadLine();
        }
    }
}
