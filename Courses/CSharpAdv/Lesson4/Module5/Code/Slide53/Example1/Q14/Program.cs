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

        static void F()
        {
            Console.WriteLine("c");
            Task<int> t3 = Execute();
            Task t4 = t3.ContinueWith(prevTask =>
            {
                int result = prevTask.Result;
                Console.WriteLine(result);
            });
        }

        static Task<int> Execute()
        {
            Console.WriteLine("run on calling thread");
            Task t1 = Task.Factory.StartNew(() => Thread.Sleep(1000));
            Task<int> t2 = t1.ContinueWith(_ =>
            {
                Console.WriteLine("run on callback thread");
                return 1;
            }
            );
            return t2;
        }
    }
}
