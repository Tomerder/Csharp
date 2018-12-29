using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace PerformanceCountersDiagnostics
{
    class Program
    {
        static private object _syncObject = new object();
        
        static private Queue<int> _queue = new Queue<int>();
        static private int _totalOperations;

        static void ThreadProc(object param)
        {
            try
            {
                bool isReader = (bool)param;
                while (true)
                {
                    lock (_syncObject)
                    {
                        if (isReader)
                        {
                            if (_queue.Count != 0)
                                _queue.Peek();
                        }
                        else
                        {
                            if (_queue.Count > 5)
                                _queue.Clear();
                            _queue.Enqueue(5);
                        }
                        ++_totalOperations;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void Main(string[] args)
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;

            Stopwatch stopper = Stopwatch.StartNew();
            for (int i = 0; i < 20; ++i)
            {
                Thread t = new Thread(ThreadProc);
                t.IsBackground = true;
                t.Start(i % 2 == 0);
            }
            Console.ReadLine();
            Console.WriteLine("Total operations per ms: " +
                ((float)_totalOperations) / stopper.ElapsedMilliseconds);
        }
    }
}
