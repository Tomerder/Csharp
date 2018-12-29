using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace APM
{
    /// <summary>
    /// Demonstrates the various ways to poll for asynchronous work to complete:
    /// Using the IsCompleted property, waiting for the wait handle embedded in the
    /// IAsyncResult object, and using a callback that is invoked when the operation
    /// completes.  Both an anonymous method and a regular method are used as callbacks.
    /// </summary>
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

            //Use callback without closures:
            Printer printer = new Printer();
            ar = invoker.BeginInvoke(new AsyncCallback(CalculationEnded), printer);

            //Use callback
            ar = invoker.BeginInvoke(input =>
            {
                Printer printer2 = (Printer)input.AsyncState;
                result = invoker.EndInvoke(input);
            }, printer);
            Console.Read();
        }

        private static void CalculationEnded(IAsyncResult ar)
        {
            //We need to retrieve the delegate and the state:
            AsyncResult realAR = (AsyncResult)ar;
            Printer printer = (Printer)ar.AsyncState;
            var invoker = (Func<int>)realAR.AsyncDelegate;
            //End the operation and print the result:
            int result = invoker.EndInvoke(ar);
            printer.Print(result);
        }
    }
}
