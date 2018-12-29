using System.IO;
using System.Threading;

namespace ThreadPoolExample
{
    /// <summary>
    /// A logger which asynchronously writes messages to a log file.
    /// </summary>
    class AsyncLogger
    {
        private readonly StreamWriter _writer;

        public AsyncLogger(string file)
        {
            _writer = new StreamWriter(file);
        }

        public void WriteLog(string message)
        {
            _writer.Write(message);
        }

        public void WriteLogAsync(string message)
        {
            //Dispatch the work asynchronously using the thread pool:
            ThreadPool.QueueUserWorkItem(delegate 
            {
                WriteLog(message);
            });

            ThreadPool.QueueUserWorkItem(delegate (object obj)
            {
                WriteLog(message);
            });

            ThreadPool.QueueUserWorkItem((object obj) =>
            {
                WriteLog(message);
            });

            ThreadPool.QueueUserWorkItem(_ =>
            {
                WriteLog(message);
            });
        }

        public void Close()
        {
            _writer.Close();
        }
    }
}
