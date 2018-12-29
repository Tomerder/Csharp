using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10, i => { Console.WriteLine(i); });
            Console.WriteLine("I'm done");
        }
    }
}
