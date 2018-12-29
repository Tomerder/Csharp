using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class C
    {
        public int i;
    }

    class Program
    {
        static void Main(string[] args)
        {
            C c1 = new C();
            Parallel.For(0, 100000, i => { c1.i++; });
            Console.WriteLine(c1.i);
        }
    }
}
