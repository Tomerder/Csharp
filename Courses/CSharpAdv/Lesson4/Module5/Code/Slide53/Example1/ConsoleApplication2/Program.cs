using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            F();
            Console.ReadLine();
        }

        static async void F()
        {
            await Execute();
            Console.WriteLine("Amir Adler");
        }

        static async Task Execute()
        {
            Console.WriteLine("run on calling thread");
            await Task.Factory.StartNew(() => Thread.Sleep(1000));
            Console.WriteLine("run on callback thread");
        }
    }
}
