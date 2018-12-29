using System;
using System.Threading;

namespace AdvancedSynchronization
{
    class ThreadedConsole
    {
        public static void WriteLine(string s)
        {
            Console.WriteLine("[~{0}] {1}", Thread.CurrentThread.ManagedThreadId, s);
        }
    }
}
