using System.Collections;
using System.IO;
using System.Threading;

namespace QueueExample
{
    /// <summary>
    /// Another version of the asynchronous logger which uses an internal
    /// synchronized queue of work items (log messages).  A separate thread
    /// performs the actual writing of messages to the log.
    /// </summary>
    class AsyncLogger2
    {
        private readonly StreamWriter _writer;
        private readonly Thread _thread;
        private readonly Queue _workItems;
        private bool _stop;

        public AsyncLogger2(string file)
        {
            //Note that we're using the non-generic Queue here
            //because it has a Synchronized method for automatic
            //synchronization.  We will discuss synchronization in
            //more detail later.

            _writer = new StreamWriter(file);
            _workItems = Queue.Synchronized(new Queue());
            _thread = new Thread(new ThreadStart(WriteThread));
            _thread.Start();
        }

        private void WriteThread()
        {
            while (!_stop)
            {
                //In reality, we should wait for something and not poll here
                Thread.Sleep(5);
                if (_workItems.Count > 0)
                {
                    _writer.Write(_workItems.Dequeue());
                }
            }
        }

        public void WriteLogAsync(string message)
        {
            _workItems.Enqueue(message);
        }

        public void Close()
        {
            _stop = true;
            _thread.Join();
            _writer.Close();
        }
    }
}
