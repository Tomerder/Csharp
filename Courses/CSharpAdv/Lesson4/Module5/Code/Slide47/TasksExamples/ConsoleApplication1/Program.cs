using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void F()
        {
            Thread.Sleep(2000);
            Console.WriteLine("F");
        }
        
        static void Main(string[] args)
        {
            Task t = new Task(F);
            t.Start();
            Console.WriteLine("Before Wait");
            t.Wait();
            Console.WriteLine("After Wait");
        }
    }
}

