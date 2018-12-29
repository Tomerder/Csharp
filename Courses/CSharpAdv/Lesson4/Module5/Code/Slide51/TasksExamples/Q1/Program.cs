using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return 42;
            });

            Thread.Sleep(3000);

            Task t2 = t1.ContinueWith(prevTask => Console.WriteLine(prevTask.Result));

            Console.WriteLine("Amir Adler");
            Console.ReadLine();
        }
    }
}
