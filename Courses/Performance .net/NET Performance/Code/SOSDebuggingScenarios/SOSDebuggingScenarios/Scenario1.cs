using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SOSDebuggingScenarios
{
    #region Don't look here, cheater!
    static class LCE
    {
        public static event EventHandler Event;
    }

    class MyResource
    {
        private byte[] _data = new byte[1000];

        public MyResource()
        {
            LCE.Event += Method;
        }

        private void Method(object sender, EventArgs e)
        {
        }

        public void Work() { }
    } 
    #endregion

    static class Scenario1
    {
        static public void Run()
        {
            int count = 0;
            while (true)
            {
                Thread.Sleep(10);
                MyResource rsrc = new MyResource();
                rsrc.Work();

                if (++count % 100 == 0)
                    Console.WriteLine("Memory in use: " + GC.GetTotalMemory(false));
            }
        }
    }
}
