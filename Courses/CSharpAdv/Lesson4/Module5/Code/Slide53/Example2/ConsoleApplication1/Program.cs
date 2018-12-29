using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute();
            Console.ReadLine();
        }

        static async Task Execute()
        {
            try
            {
                Console.WriteLine("run on calling thread");
                await Task.Factory.StartNew(() =>
                {
                    throw new Exception("aaa");
                });
                Console.WriteLine("run on callback thread");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
