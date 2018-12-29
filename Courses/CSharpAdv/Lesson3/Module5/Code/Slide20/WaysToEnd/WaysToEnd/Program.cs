﻿using System;
using System.Threading;

namespace APM
{
    /// <summary>
    /// Demonstrates the various ways to poll for asynchronous work to complete:
    /// Using the IsCompleted property, waiting for the wait handle embedded in the
    /// IAsyncResult object, and using a callback that is invoked when the operation completes.
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

            //Use callback
            ar = invoker.BeginInvoke(input =>
                {
                    result = invoker.EndInvoke(input);
                }, null);
        }
    }
}