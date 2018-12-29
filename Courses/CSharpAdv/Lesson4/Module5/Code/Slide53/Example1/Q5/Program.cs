using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a");
            F();
            Console.WriteLine("b");
            Console.ReadLine();
        }

        static void F()
        {
            Console.WriteLine("c");
            Execute();
            Console.WriteLine("Amir Adler");
        }

        static Task Execute()
        {
            Console.WriteLine("run on calling thread");
            Task t1 = Task.Factory.StartNew(() => Thread.Sleep(1000));
            Task t2 = t1.ContinueWith(prevTask => Console.WriteLine("run on callback thread"));
            return t2;
        }
    }
}
