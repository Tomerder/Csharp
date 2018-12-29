using System;
using System.Threading;

namespace InvokingEvents
{
    /// <summary>
    /// In this demo, the handlers are invoked asynchronously on different threads,
    /// none of them being the main thread which caused the event invocation.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            EventHandler<EventArgs> handler1 = delegate { Console.WriteLine("Handler 1 in thread " + Thread.CurrentThread.ManagedThreadId); };
            EventHandler<EventArgs> handler2 = delegate { Console.WriteLine("Handler 2 in thread " + Thread.CurrentThread.ManagedThreadId); };

            AsyncEventSource.Register(handler1);
            AsyncEventSource.Register(handler2);

            AsyncEventSource.Invoke();

            AsyncEventSource.Unregister(handler1);
            AsyncEventSource.Unregister(handler2);
            Console.ReadLine();
        }
    }
}
