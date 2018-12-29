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
                ThreadPool.QueueUserWorkItem(PrintChar, c);
                ThreadPool.QueueUserWorkItem(obj =>                {
                    char dc = (char)obj;
                    Console.WriteLine(dc);
                }, c);
                ThreadPool.QueueUserWorkItem(obj => {
                    Console.WriteLine(obj);
                }, c);
            }

            Console.Read();
        }

        static void PrintChar(object obj)
        {
            char c = (char)obj;
            Console.WriteLine(c);
        }
    }
}
