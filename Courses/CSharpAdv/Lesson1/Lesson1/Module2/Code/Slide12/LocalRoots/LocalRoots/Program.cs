using System;

namespace LocalRoots
{
    class Program
    {
        static void Main(string[] args)
        {
            C1 c = new C1();
            C2 c2 = new C2() { C = c };
            c = null;
            Console.WriteLine("a");
            GC.Collect();
            Console.ReadLine();
            c2.C = null;
            c2 = null;
            Console.WriteLine("b");
            GC.Collect();
            Console.ReadLine();
            Console.WriteLine("c");
        }
    }
}
