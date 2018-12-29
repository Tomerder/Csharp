using System;
using System.Threading;

namespace AdvancedSynchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            DoInParallel(
                delegate { ThreadedConsole.WriteLine("Hi from action 1"); },
                delegate { ThreadedConsole.WriteLine("Hi from action 2"); },
                delegate { ThreadedConsole.WriteLine("Hi from action 3"); }
                );
        }

        /// <summary>
        /// Performs multiple actions in parallel and returns when all actions are done.
        /// </summary>
        /// <param name="actions">The actions to perform in parallel.</param>
        public static void DoInParallel(params Action[] actions)
        {
            CountdownEvent counter = new CountdownEvent(actions.Length);
            foreach (Action action in actions)
            {
                ThreadPool.QueueUserWorkItem(delegate { action(); counter.Set(); });
            }
            counter.Wait();
        }
    }
}
