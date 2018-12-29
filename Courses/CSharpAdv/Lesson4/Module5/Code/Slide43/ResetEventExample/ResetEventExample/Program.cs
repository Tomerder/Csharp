using System;
using System.Threading;

namespace ResetEventExample
{
    /// <summary>
    /// Demonstrates the ManualResetEvent
    /// which is used here to receive a notification when a thread pool work item completes
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ManualResetEvent notify = new ManualResetEvent(false);
            DoAndLetMeKnow(delegate
            {
                Console.Write("In thread pool thread...");
                Thread.Sleep(2000);
                Console.WriteLine("...done!");
            }, notify);
            notify.WaitOne();
            Console.WriteLine("Main thread also thinks you're done.");

        }

        /// <summary>
        /// Performs work asynchronously and sets the specified event handle
        /// when it is done.
        /// </summary>
        /// <param name="action">The work to perform.</param>
        /// <param name="event">The event to set.</param>
        public static void DoAndLetMeKnow(Action action, EventWaitHandle @event)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                action();
                @event.Set();
            });
        }
    }
}
