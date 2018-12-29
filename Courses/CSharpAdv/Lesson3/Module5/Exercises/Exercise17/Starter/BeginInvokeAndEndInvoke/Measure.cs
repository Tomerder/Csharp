using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace APM
{
    /// <summary>
    /// Contains a convenience method for measuring the time it takes
    /// a piece of code to run and then printing the time to the console
    /// (in milliseconds).
    /// </summary>
    static class Measure
    {
        public static void It(Action action, string description)
        {
            Stopwatch sw = Stopwatch.StartNew();
            action();
            Console.WriteLine("Action {0} took {1}ms", description, sw.ElapsedMilliseconds);
        }
    }
}
