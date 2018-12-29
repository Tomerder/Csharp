using System;
using System.Threading;

namespace ConsoleApplication3
{
    class Program
    {
        static void F(object o)
        {
            int x = (int)o;
            for (int i = 0; i < 100; i++)
            {
                Console.Write(x);
                Thread.Sleep(0)
            }
        }

        static void Main(string[] args)
        {
            ParameterizedThreadStart pts1 = new ParameterizedThreadStart(F);
            Thread t1 = new Thread(pts1);
            t1.Start(1);
            ParameterizedThreadStart pts2 = new ParameterizedThreadStart(F);
            Thread t2 = new Thread(pts2);
            t2.Start(2);
            Console.Read();
        }
    }
}
