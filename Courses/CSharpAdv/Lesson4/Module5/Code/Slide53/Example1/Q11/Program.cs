using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a " + Thread.CurrentThread.ManagedThreadId);
            F();
            Console.WriteLine("b " + Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }

        static async void F()
        {
            Console.WriteLine("c " + Thread.CurrentThread.ManagedThreadId);
            await Execute();
            Console.WriteLine("Amir Adler " + Thread.CurrentThread.ManagedThreadId);
        }

        static async Task Execute()
        {
            Console.WriteLine("run on calling thread " + Thread.CurrentThread.ManagedThreadId);
            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("After sleep: " + Thread.CurrentThread.ManagedThreadId);
            });
            Console.WriteLine("run on callback thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
