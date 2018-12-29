using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.CompilerServices;

namespace TimerDemo
{
    class Program
    {
        static void OnTimer(object dummy)
        {
            Console.WriteLine(DateTime.Now.TimeOfDay);
/*timer thread*/GC.Collect(); //Force GC right now
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void MyKeepAlive(object obj) { }

        static void Main(string[] args)
        {
            Timer t = new Timer(OnTimer, null, 0, 1000);
/*main thread is here*/Console.ReadLine();

//we want to prevent collection of t, until this line (after Console.ReadLine)
            //it will be done by Dummy use of t as a parameter for blank func (must not be inlined optimized, so use of t as parameter wont be ingnored) 
            //GC.KeepAlive(t);
            MyKeepAlive(t);
        }
    }
}
