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

        static async Task Execute()
        {
            Console.WriteLine("run on calling thread");
            Task t = new Task(() => Console.WriteLine("d"));
            await t;
            Console.WriteLine("run on callback thread");
        }
    }
}
