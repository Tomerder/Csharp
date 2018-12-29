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
                WaitCallback methodToCall = new WaitCallback(PrintChar);
                ThreadPool.QueueUserWorkItem(methodToCall, c);
            }
        }

        static void PrintChar(object obj)
        {
            char c = (char)obj;
            Console.WriteLine(c);
        }
    }
}
