using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void F(object state)
        {
            Thread.Sleep(2000);
            Console.WriteLine(state);
        }

        static void Main(string[] args)
        {
            Task t = new Task(F, "Amir Adler");
            t.Start();
            Console.WriteLine("Before Wait");
            t.Wait();
            Console.WriteLine("After Wait");
        }
    }
}
