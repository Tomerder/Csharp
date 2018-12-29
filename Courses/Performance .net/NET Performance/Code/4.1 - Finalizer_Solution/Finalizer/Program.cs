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
            Thread.Sleep(100);
        }
    }

    public class Employee
    {
        private Schedule _schedule = new Schedule();
        private static int _finalizationCount;

        // Work is Sleep.  That's what .NET employees do.
        public void Work() { Thread.Sleep(10); }

        ~Employee()
        {
            _schedule.FreeData();

            if (++_finalizationCount % 50 == 0)
                Console.WriteLine("Finalized {0} objects", _finalizationCount);
        }
    }

    class Program
    {
        private static int _creationCount;

        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(10);

                Employee emp = new Employee();
                emp.Work();

                if (++_creationCount % 100 == 0)
                {
                    Console.WriteLine("Created {0} objects", _creationCount);
                    Console.WriteLine("Total memory in use (KB): {0}", GC.GetTotalMemory(false) / 1024);
                }
            }
        }
    }
}
