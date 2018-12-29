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
            _del(4);
        }
    }
}
