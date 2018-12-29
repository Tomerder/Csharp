using System;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 6, 7, 8, 9 };

            Parallel.ForEach(arr, i => Console.WriteLine(i));
            Console.WriteLine("I'm done");
        }
    }
}
