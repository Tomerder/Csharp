using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ParallelAndConcurrent
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<int> randomOrder = new ConcurrentBag<int>();
            
            Parallel.For(0, 9, n =>
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
