using System;
using System.Threading;

namespace APM
{
    /// <summary>
    /// Demonstrates various ways to poll for asynchronous work to complete:
    /// Using the IsCompleted property and waiting for the wait handle embedded in the IAsyncResult object
    class Program
    {
        static void Main(string[] args)
        {
            PrimeNumberCalculator calc = new PrimeNumberCalculator(5, 100000);
            Func<int> invoker = calc.Calculate;

            //Poll for IsCompleted property:
            IAsyncResult ar = invoker.BeginInvoke(null, null);
            while (!ar.IsCompleted)
                Thread.Sleep(100);
            int result = invoker.EndInvoke(ar);

            //Wait for the wait handle:
            ar = invoker.BeginInvoke(null, null);
            ar.AsyncWaitHandle.WaitOne();
            result = invoker.EndInvoke(ar);
        }
    }
}
