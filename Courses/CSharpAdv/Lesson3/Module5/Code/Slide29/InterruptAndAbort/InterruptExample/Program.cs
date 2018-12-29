using System;
using System.Threading;

namespace InterruptExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(F);
            t.Start();
            Thread.Sleep(1000);
            t.Interrupt();
        }

        static void F()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("a");
                    Thread.Sleep(10);
                }
            }

            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("b");
        }
    }
}
