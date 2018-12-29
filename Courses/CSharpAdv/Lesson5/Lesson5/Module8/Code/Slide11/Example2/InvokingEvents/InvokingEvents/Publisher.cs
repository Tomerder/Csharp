using System;
using System.Threading;

namespace InvokingEvents
{
    delegate void Del(int x);

    class Publisher
    {
        public event Del _del = delegate { };

        public void RaiseEvent()
        {
            Invoke(4);
        }

        private void Invoke(params object[] args)
        {
            foreach (Delegate d in _del.GetInvocationList())
            {
                ThreadPool.QueueUserWorkItem(
                    delegate { d.DynamicInvoke(args); });
            }
        }
    }
}
