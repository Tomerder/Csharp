using System;
using System.Threading;

namespace ThreadPoolExample
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                WaitCallback methodToCall = new WaitCallback(ThreadProc);
                ThreadPool.QueueUserWorkItem(methodToCall, i);
            }
            Thread.Sleep(1000);
        }

        static void ThreadProc(object obj)
        {
            int i = (int)obj;
            Console.WriteLine(i);
        }
    }
}
