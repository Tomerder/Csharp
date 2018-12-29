using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerEx
{
    class Program
    {
        static Timer timer2;

        static void Main(string[] args)
        {
            Timer timer = new Timer(Func, null, 1000, 1000);
            FuncCopy(timer);

            Console.ReadLine();
            //GC.KeepAlive(timer);   
        }

        private static void FuncCopy(Timer timer)
        {
            //timer2 = timer; //timer2 will never be collected because it is rooted in static object referance 
        }

        private static void Func(object _dummy)
        {
            Console.WriteLine(DateTime.Now.TimeOfDay);
            BigClass big = new BigClass(); //allocate in order for GC to work upon space in heap is low
            //GC.collect()  //force GC
        }  
    }

    class BigClass
    {
        int[] dummy;

        public BigClass()
        {
            dummy = new int[500000];
        }
    }

}
