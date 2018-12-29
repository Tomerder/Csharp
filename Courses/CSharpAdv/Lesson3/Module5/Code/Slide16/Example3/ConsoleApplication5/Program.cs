using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication5
{
    delegate int Del(int x);

    class Program
    {
        static void Main(string[] args)
        {
            Del del1 = new Del(F);
            IAsyncResult iar1 = del1.BeginInvoke(6000, null, null);
            Del del2 = new Del(F);
            IAsyncResult iar2 = del2.BeginInvoke(4000, null, null);
            Del del3 = new Del(F);
            IAsyncResult iar3 = del3.BeginInvoke(2000, null, null);
            //int i1 = del1.EndInvoke(iar1);
            //int i2 = del2.EndInvoke(iar2);
            //int i3 = del3.EndInvoke(iar3);
            //while (iar1.IsCompleted == false) { }
            //while (iar2.IsCompleted == false) { }
            //while (iar3.IsCompleted == false) { }
            WaitHandle[] handles = { iar1.AsyncWaitHandle, iar2.AsyncWaitHandle, iar3.AsyncWaitHandle };
            WaitHandle.WaitAll(handles);
        }

        private static int F(int timeToSleep)
        {
            Thread.Sleep(timeToSleep);
            Console.WriteLine(timeToSleep);
            return timeToSleep;
        }
    }
}
