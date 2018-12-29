using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int> f = () =>
            {
                Thread.Sleep(2000);
                return 42;
            };

            Task<int> t = Task.Factory.StartNew(f);
            Console.WriteLine("Amir Adler");
            int res = t.Result;
            Console.WriteLine(res);
        }
    }
}
