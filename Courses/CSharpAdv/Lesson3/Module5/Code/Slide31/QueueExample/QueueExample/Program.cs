using System;
using System.IO;
using System.Threading;

namespace QueueExample
{
    class Program
    {
        /// <summary>
        /// Demonstrates how work can be queued asynchronously to a separate thread
        /// which performs the logging work.  Synchronization on the underlying stream
        /// is guaranteed by the fact that only one thread performs the logging and
        /// takes the items from a queue, so they arrive in-order (guaranteed).
        /// </summary>
        static void Main(string[] args)
        {
            AsyncLogger2 logger = new AsyncLogger2("log.txt");
            for (int i = 0; i < 100; ++i)
            {
                logger.WriteLogAsync(i.ToString() + " ");
            }
            Thread.Sleep(TimeSpan.FromSeconds(3));  //Wait for logging to complete
            logger.Close();
            Console.WriteLine(File.ReadAllText("log.txt"));
        }
    }
}
