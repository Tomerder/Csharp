using System;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void F(object o)
        {
            int x = (int)o;
            for (int i = 0; i < 100; i++)
            {
                Console.Write(x);
                Thread.Sleep(1);
            }
        }

        static void Main(string[] args)
        {
            ParameterizedThreadStart pts1 = new ParameterizedThreadStart(F);
            Thread t1 = new Thread(pts1);
            t1.IsBackground = true;
            t1.Start(1);
            //t1.Join();
            ParameterizedThreadStart pts2 = new ParameterizedThreadStart(F);
            Thread t2 = new Thread(pts2);
            t2.IsBackground = true;
            t2.Start(2);
            //Console.Read();
            //t2.Join();
        }
    }
}
