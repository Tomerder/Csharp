using System;
using System.Threading;

namespace RegisterExample
{
    class Publisher
    {
        public event Action _myEvent = delegate { };
        private Timer _timer;

        public Publisher()
        {
            _timer = new Timer(RaiseEvent, null, 1000, 1000);
        }

        private void RaiseEvent(object dummy)
        {
            _myEvent();
        }
    }
}
