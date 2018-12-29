using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//compile on RELEASE MODE -> timer can be collected before end of Main

namespace TimerEx
{
    class Program
    {
        static Timer timer2;
        const int SIZE = 100000;

        static void Main(string[] args)
        {
            Timer timer = new Timer(Func, null, 1000, 1000);
            FuncCopy(timer);

            Console.ReadLine();
            //GC.KeepAlive(timer);   
        }

        private static void FuncCopy(Timer timer)
        {
            timer2 = timer; //timer2 will never be collected because it is rooted in static object referance 
        }

        private static void Func(object _dummy)
        {            
            BigClass big = new BigClass(); //allocate in order for GC to work upon space in heap is low
            Console.WriteLine(DateTime.Now.TimeOfDay + " ,Total mem allocated : " + BigClass.totalSize + " ,alloc on Gen : " + GC.GetGeneration(big) + " ,collections count : " + GC.CollectionCount(0) + " ,Total mem : " + GC.GetTotalMemory(false) );
            //GC.collect()  //force GC
        }

        class BigClass
        {
            int[] dummy;

            public static int counter = 0;
            public static int totalSize = 0;

            public BigClass()
            {
                dummy = new int[SIZE];
                counter++;
                totalSize = SIZE * counter * sizeof(int);
            }
        }
    }

}
