using System;
using System.Threading;

namespace InvokingEvents
{
    /// <summary>
    /// Asynchronously invokes a multi-cast delegate.  Each handler
    /// in its invocation list is invoked separately in a completely
    /// asynchronous fashion.  Return values are not cached and are not
    /// returned, so delegates using this facility should not have a 
    /// return value.
    /// </summary>
    public sealed class AsyncInvoker
    {
        private readonly MulticastDelegate _del;

        public AsyncInvoker(MulticastDelegate del)
        {
            _del = del;
        }

        public void Invoke(params object[] args)
        {
            Delegate[] invocationList = _del.GetInvocationList();
            foreach (Delegate del in invocationList)
            {
                ThreadPool.QueueUserWorkItem(delegate { del.DynamicInvoke(args); });
            }
        }
    }
}
