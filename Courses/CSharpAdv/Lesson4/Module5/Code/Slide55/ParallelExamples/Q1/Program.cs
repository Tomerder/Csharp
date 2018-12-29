using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 10, i => 
            {
                if (i != 5)
                    Console.WriteLine(i);
                else
                    throw new Exception("aaa");
            });
            Console.WriteLine("I'm done");
        }
    }
}
