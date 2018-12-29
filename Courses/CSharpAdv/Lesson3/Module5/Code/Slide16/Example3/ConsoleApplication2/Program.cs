using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication2
{
    delegate int Del(int x);

    class Program
    {
        static void Main(string[] args)
        {
            DateTime dtBefore = DateTime.Now;
            Del del1 = new Del(F);
            IAsyncResult iar1 = del1.BeginInvoke(3000, null, null);
            Del del2 = new Del(F);
            IAsyncResult iar2 = del2.BeginInvoke(2000, null, null);
            Del del3 = new Del(F);
            IAsyncResult iar3 = del3.BeginInvoke(1000, null, null);
            int i1 = del1.EndInvoke(iar1);
            int i2 = del2.EndInvoke(iar2);
            int i3 = del3.EndInvoke(iar3);
            DateTime dtAfter = DateTime.Now;
            TimeSpan dt = dtAfter - dtBefore;
            Console.WriteLine(i1);
            Console.WriteLine(i2);
            Console.WriteLine(i3);
            Console.WriteLine(dt.Seconds);
        }

        private static int F(int timeToSleep)
        {
            Thread.Sleep(timeToSleep);
            return timeToSleep;
        }
    }
}
