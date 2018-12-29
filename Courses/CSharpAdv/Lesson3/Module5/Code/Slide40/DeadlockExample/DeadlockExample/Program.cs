using System;
using System.Threading;

namespace DeadlockExample
{
    /// <summary>
    /// Shows how a deadlock might ensue if resources are not locked in the same order.
    /// In this example, thread #1 acquires locks in the order c1, c2 and thread #2 
    /// acquires locks in the order c2, c1.  As a result, if sufficient time elapses,
    /// the threads are deadlocked because they are waiting for each other.  Detecting
    /// and resolving deadlocks is outside the scope of this demo, but one example strategy
    /// which prevents deadlocks such as this one is to perform lock ordering - always
    /// acquire locks in the same order.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Counter c1 = new Counter();
            Counter c2 = new Counter();

            ThreadPool.QueueUserWorkItem(delegate
            {
                lock (c1)
                {
                    Thread.Sleep(500);
                    for (int i = 0; i < 100000; ++i) c1.Next();
                    lock (c2)
                    {
                        for (int i = 0; i < 100000; ++i) c2.Next();
                    }
                }
                Console.WriteLine("Thread 1 done");
            });
            ThreadPool.QueueUserWorkItem(delegate
            {
                lock (c2)
                {
                    Thread.Sleep(500);
                    for (int i = 0; i < 100000; ++i) c2.Next();
                    lock (c1)
                    {
                        for (int i = 0; i < 100000; ++i) c1.Next();
                    }
                }
                Console.WriteLine("Thread 2 done");
            });
            Console.Write("Wait a few seconds for the threads to be done.  If nothing is printed, there's a deadlock");
            Console.ReadLine();
        }
    }
}
