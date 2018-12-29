using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Finalizer
{
    public class Schedule
    {
        private byte[] _data = new byte[10000];

        public void FreeData()
        {
            Thread.Sleep(20);
        }
    }

    public class Employee
    {
        private Schedule _schedule = new Schedule();
        private static int _finalizationCount;

        // Work is Sleep.  That's what .NET employees do.
        public void Work() { Thread.Sleep(10);  }

        ~Employee()
        {
            _schedule.FreeData();
        }
    }

    class Program
    {
        private static int _creationCount;

        static void Main(string[] args)
        {
            while (true)
            {
                Employee emp = new Employee();
                emp.Work();

                //GC.Collect();
                //GC.WaitForPendingFinalizers();

                //TODO: Increment the _creationCount counter and output it.
                //TODO: Output the total memory occupied by this process using GC.GetTotalMemory(false).
                //if (++_creationCount % 50 == 0)
                //    Console.WriteLine("Memory usage: " + GC.GetTotalMemory(false)/1024 + " KB");
            }
        }
    }
}
