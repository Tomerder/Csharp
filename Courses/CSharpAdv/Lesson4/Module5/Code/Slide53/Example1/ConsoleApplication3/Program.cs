using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
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

        static async void F()
        {
            Console.WriteLine("c");
            int result = await Execute();
            Console.WriteLine(result);
        }

        static async Task<int> Execute()
        {
            Console.WriteLine("run on calling thread");
            await Task.Factory.StartNew(() => Thread.Sleep(1000));
            Console.WriteLine("run on callback thread");
            return 1;
        }
    }
}
