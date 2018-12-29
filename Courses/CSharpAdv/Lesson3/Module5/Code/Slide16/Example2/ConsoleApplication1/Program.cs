using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    delegate void Del();

    class Program
    {
        static void Main(string[] args)
        {
            Del del = new Del(F);
            del.BeginInvoke(null, null);
            Console.WriteLine("Main");
        }

        static void F()
        {
            Thread.Sleep(1000);
            Console.WriteLine("F");
        }
    }
}
