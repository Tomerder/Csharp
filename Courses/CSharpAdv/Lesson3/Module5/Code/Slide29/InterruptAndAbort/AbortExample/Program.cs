using System;
using System.Threading;

namespace AbortExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(F);
            t.Start();
            Thread.Sleep(1000);
            t.Abort();
        }

        static void F()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("a");
                }
            }

            catch (ThreadAbortException e)
            {
                Console.WriteLine(e.Message);
                // Thread.ResetAbort();
            }

            Console.WriteLine("b");
        }
    }
}
