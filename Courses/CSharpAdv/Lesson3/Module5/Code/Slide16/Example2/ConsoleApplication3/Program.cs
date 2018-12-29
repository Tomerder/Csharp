using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication3
{
    delegate string Del();

    class Program
    {
        static void Main(string[] args)
        {
            Del del = new Del(F);
            IAsyncResult iar = del.BeginInvoke(null, null);
            string s = del.EndInvoke(iar);
            Console.WriteLine(s);
        }

        static string F()
        {
            Thread.Sleep(1000);
            Console.WriteLine("F");
            return "a";
        }
    }
}
