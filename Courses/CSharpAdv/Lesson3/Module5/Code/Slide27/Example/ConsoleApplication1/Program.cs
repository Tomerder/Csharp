using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
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
            F1();
            F2();
            Console.Read();
        }
    }
}
