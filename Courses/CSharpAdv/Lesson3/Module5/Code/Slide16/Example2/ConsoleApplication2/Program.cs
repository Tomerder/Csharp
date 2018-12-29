using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication2
{
    delegate void Del();

    class Program
    {
        static void Main(string[] args)
        {
            Del del = new Del(F);
            IAsyncResult iar = del.BeginInvoke(null, null);
            del.EndInvoke(iar);
            Console.WriteLine("Main");
        }

        static void F()
        {
            Thread.Sleep(1000);
            Console.WriteLine("F");
        }
    }
}
