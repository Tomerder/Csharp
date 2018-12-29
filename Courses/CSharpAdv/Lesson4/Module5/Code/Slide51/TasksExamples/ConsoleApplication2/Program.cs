using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t1 = Task.Factory.StartNew(() =>
            {
                throw new Exception("aaa");
                return 42;
            });

            Task t2 = t1.ContinueWith(prevTask =>
            {
                if (prevTask.Exception != null)
                    Console.WriteLine("Failed {0}", prevTask.Exception.InnerException.Message);
                else
                    Console.WriteLine("Returned {0}", prevTask.Result);
            });

            Console.ReadLine();
        }
    }
}
