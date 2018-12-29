using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication6
{
    delegate int Del(int x);

    class Program
    {
        static void Main(string[] args)
        {
            Del del1 = new Del(F);
            IAsyncResult iar1 = del1.BeginInvoke(6000, null, null);
            Del del2 = new Del(F);
            IAsyncResult iar2 = del2.BeginInvoke(3000, null, null);
            Del del3 = new Del(F);
            IAsyncResult iar3 = del3.BeginInvoke(1000, null, null);
            //WaitHandle[] handles = { iar1.AsyncWaitHandle, iar2.AsyncWaitHandle, iar3.AsyncWaitHandle };
            //WaitHandle.WaitAll(handles);
            WaitHandle[] handles = { iar1.AsyncWaitHandle, iar3.AsyncWaitHandle };
            WaitHandle.WaitAny(handles);
        }

        private static int F(int timeToSleep)
        {
            Thread.Sleep(timeToSleep);
            Console.WriteLine(timeToSleep);
            return timeToSleep;
        }
    }
}
