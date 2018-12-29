using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<DateTime> f = () =>
            {
                Thread.Sleep(2000);
                return DateTime.Now;
            };

            Task<DateTime> t2 = Task.Run(f);
            Console.WriteLine("Amir Adler");
            DateTime x = t2.Result;
            Console.WriteLine(x);
        }
    }
}
