﻿using System;
using System.IO;
using System.Threading;

namespace ThreadPoolExample
{
    /// <summary>
    /// Demonstrates how work can be queued asynchronously using the thread pool,
    /// but also shows some of the problems with this approach:
    ///     1) There is no way to receive a notification when the work is complete,
    ///        meaning that the logger.Close() call is relying on sheer luck.
    ///     2) There is no guarantee of synchronization or ordering on the underlying
    ///        stream, so we're seeing output out of order.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AsyncLogger logger = new AsyncLogger("log.txt");
            for (int i = 0; i < 100; ++i)
            {
                logger.WriteLogAsync(i.ToString() + " ");
            }
            Thread.Sleep(TimeSpan.FromSeconds(1));  //Wait for logging to complete
            logger.Close();
            Console.WriteLine(File.ReadAllText("log.txt"));
        }
    }
}
