using System;

namespace GcExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GC.CollectionCount(0));
            Console.WriteLine(GC.CollectionCount(2));
            GC.Collect(0, GCCollectionMode.Forced);
            Console.WriteLine(GC.CollectionCount(0));
            Console.WriteLine(GC.CollectionCount(2));
            GC.Collect(2, GCCollectionMode.Forced);
            Console.WriteLine(GC.CollectionCount(0));
            Console.WriteLine(GC.CollectionCount(2));
        }
    }
}
