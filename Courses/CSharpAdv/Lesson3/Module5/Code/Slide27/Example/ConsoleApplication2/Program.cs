using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication2
{
    class Program
    {
        private static void F1()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("F1");
                Thread.Sleep(100);
            }
        }

        private static void F2()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("F2");
                Thread.Sleep(100);
            }
        }

        static void Main(string[] args)
        {
            ThreadStart ts1 = new ThreadStart(F1);
            Thread t1 = new Thread(ts1);
            t1.Start();
            ThreadStart ts2 = new ThreadStart(F2);
            Thread t2 = new Thread(ts2);
            t2.Start();
            Console.Read();
        }
    }
}
