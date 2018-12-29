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
                ParameterizedThreadStart pts1 = new ParameterizedThreadStart(PrintChar);
                Thread t1 = new Thread(pts1);
                t1.Start(c);
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
