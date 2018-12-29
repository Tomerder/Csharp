using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Action a = () => 
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Amir Adler"); 
                };

            Task t = Task.Run(a);
            Console.WriteLine("Before Wait");
            t.Wait();
            Console.WriteLine("After Wait");
        }
    }
}
