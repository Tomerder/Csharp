using System;
using System.Threading;

namespace PulseAndWait
{
    /// <summary>
    /// Demonstrates a simple producer-consumer scenario using a shared queue
    /// which internally uses Monitor.Pulse and Monitor.Wait to ensure that consumers
    /// blocked for input are woken up when input arrives from the producer.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            WorkQueue<int> queue = new WorkQueue<int>();
            Thread producer = new Thread(() =>
            {
                while (true)
                {
                    queue.Enqueue(42);
                    Thread.Sleep(10);
                }
            });
            Thread consumer = new Thread(() =>
            {
                while (true)
                {
                    queue.Dequeue();
                    Console.Write(".");
                }
            });
            producer.Start();
            consumer.Start();

            Console.ReadLine();
            producer.Abort(); consumer.Abort(); //Don't do this in a real application!
        }
    }
}
