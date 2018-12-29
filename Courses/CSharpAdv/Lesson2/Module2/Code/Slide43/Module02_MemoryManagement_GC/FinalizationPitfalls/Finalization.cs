using System;
using System.Threading;

namespace FinalizationPitfalls
{
    /// <summary>
    /// All the demos in this section should be run in Release mode, to ensure
    /// that the garbage collector collects local references as soon as they
    /// are no longer used.
    /// </summary>
    class Finalization
    {
        /// <summary>
        /// Uncomment one line at a time to see a demonstration of the various
        /// scenarios - race condition, 
        /// deadlock 
        /// </summary>
        static void Main(string[] args)
        {
            //MemoryLeak();
            Deadlock();

        }

        /// <summary>
        /// Leaks memory by creating objects at a faster rate than they can
        /// be finalized.  The Resource1 constructor takes 10ms but its finalizer
        /// takes 20ms, producing a steady memory leak.
        /// </summary>
        private static void MemoryLeak()
        {
            for (int i = 1; !Console.KeyAvailable; ++i)
            {
                new Resource1(); //Create and immediately destroy

                if (i % 100 == 0)
                    Console.WriteLine((GC.GetTotalMemory(false) / 1024) + " KB managed memory in use");
            }
        }

        /// <summary>
        /// Simulates a deadlock by acquiring a lock and waiting for the
        /// finalizer to complete while at the same time the finalizer requires
        /// the same lock.  The main application thread and the finalizer
        /// thread enter a deadly embrace.
        /// </summary>
        private static void Deadlock()
        {
            lock (typeof(Resource2))
            {
                new Resource2();    //Create and immediately destroy
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
