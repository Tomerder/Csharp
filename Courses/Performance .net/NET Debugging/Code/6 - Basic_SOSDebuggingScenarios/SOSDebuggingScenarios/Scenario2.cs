using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SOSDebuggingScenarios
{
    #region Don't look here, cheater!
    class MyResource2
    {
        private byte[] _data = new byte[1000];

        public MyResource2()
        {
        }

        public void Work() { }

        ~MyResource2() { Thread.Sleep(20); }
    } 
    #endregion

    static class Scenario2
    {
        static public void Run()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(10);
                MyResource2 rsrc = new MyResource2();
                rsrc.Work();

                if (++count % 100 == 0)
                    Console.WriteLine("Memory in use: " + GC.GetTotalMemory(false));
            }
        }
    }
}
