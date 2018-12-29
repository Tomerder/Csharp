using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SOSDebuggingScenarios
{
    static class Scenario3
    {
        static private void Foo(object o) { }

        static public void Run()
        {
            while (true)
            {
                Thread.Sleep(0);
                ThreadPool.QueueUserWorkItem(Foo);
            }
        }
    }
}
