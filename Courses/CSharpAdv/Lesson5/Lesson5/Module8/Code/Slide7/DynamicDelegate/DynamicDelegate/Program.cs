using System;

namespace DynamicDelegate
{
    /// <summary>
    /// In this demo, the dynamic logger framework is used to invoke
    /// the log method of the Box class through a dynamic delegate.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box { Name = "Chips", Volume = 2.0m };
            DynamicLogger.Log(Console.Out, box);
        }
    }
}
