using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demo1();
            // Demo2();
            // Demo3();
             Demo4();
            // Demo5();
        }

        private static void Demo1()
        {
            Parallel.Invoke(
                () => Console.WriteLine("a"),
                () => Console.WriteLine("b"));
        }

        private static void Demo2()
        {
            Parallel.For(0, 10, x => Console.WriteLine(x));
        }

        private static void Demo3()
        {
            Parallel.For(0, 10, x => Console.WriteLine(Thread.CurrentThread.ManagedThreadId));
        }

        private static void Demo4()
        {
            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2
            };

            Parallel.For(0, 10, options, x => Console.WriteLine(Thread.CurrentThread.ManagedThreadId));
        }

        private static void Demo5()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Parallel.ForEach<int>(list, x => Console.WriteLine(x));
        }
    }   
}
