using System;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            F();
            Console.Read();
        }

        static void F()
        {
            string s = "abcdefgh";

            foreach (char c in s.ToCharArray())
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Thread.Sleep(10000);
                    PrintChar(c);
                });
            }

            Console.WriteLine("1");
        }

        static void PrintChar(char c)
        {
            Console.WriteLine(c);
        }
    }
}
