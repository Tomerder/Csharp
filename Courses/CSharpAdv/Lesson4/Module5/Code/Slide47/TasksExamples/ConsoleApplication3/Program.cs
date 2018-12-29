using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = new Task((state) => 
                {
                    Thread.Sleep(2000);
                    Console.WriteLine(state);
                }, "Amir Adler");

            t.Start();
            Console.WriteLine("Before Wait");
            t.Wait();
            Console.WriteLine("After Wait");
        }
    }
}
