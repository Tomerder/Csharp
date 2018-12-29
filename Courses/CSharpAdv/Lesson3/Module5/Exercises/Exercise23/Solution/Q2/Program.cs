using System;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "abcdefgh";

            foreach (char c in s.ToCharArray())
            {
                ThreadPool.QueueUserWorkItem(_ => PrintChar(c));
            }

            Console.Read();
        }

        static void PrintChar(char c)
        {
            Console.WriteLine(c);
        }
    }
}
