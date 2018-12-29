using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dtBefore = DateTime.Now;
            int i1 = F(3000);
            int i2 = F(2000);
            int i3 = F(1000);
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
