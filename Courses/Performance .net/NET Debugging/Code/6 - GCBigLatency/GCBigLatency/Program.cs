using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GCBigLatency
{
    class Program
    {
        static volatile int i;

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void Foo()
        {
            for (i = 0; ; ++i) ;
        }

        static void Main(string[] args)
        {
            Thread churner1 = new Thread(() =>
            {
                Foo();
            });
            churner1.Start();

            while (true)
            {
                Thread.Sleep(1000);

                Stopwatch sw = Stopwatch.StartNew();
                GC.Collect(0);
                Console.WriteLine("gen 0 GC took: " + sw.Elapsed.TotalMilliseconds + " ms");
            }
        }
    }
}
