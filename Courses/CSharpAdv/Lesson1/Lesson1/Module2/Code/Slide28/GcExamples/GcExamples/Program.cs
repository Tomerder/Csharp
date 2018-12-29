using System;

namespace GcExamples
{
    class C { }
    
    class Program
    {
        static void Main(string[] args)
        {
            C c1 = new C();
            int i = GC.GetGeneration(c1);
            Console.WriteLine(i);
            GC.Collect();
            i = GC.GetGeneration(c1);
            Console.WriteLine(i);
        }
    }
}
