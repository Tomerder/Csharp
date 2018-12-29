using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    return 42;                
                });
            Console.WriteLine("Amir Adler");
            int res = t.Result;
            Console.WriteLine(res);
        }
    }
}
