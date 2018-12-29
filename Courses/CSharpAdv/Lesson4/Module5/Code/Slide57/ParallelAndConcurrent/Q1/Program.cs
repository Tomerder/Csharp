using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelAndConcurrent
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> randomOrder = new List<int>();

            Parallel.For(0, 99999, n =>
            {
                randomOrder.Add(n);
            });

            foreach (int i in randomOrder)
            {
                System.Console.WriteLine(i);
            }
        }
    }
}
