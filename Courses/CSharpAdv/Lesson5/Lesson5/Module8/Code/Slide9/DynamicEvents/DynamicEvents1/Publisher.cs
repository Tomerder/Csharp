using System;
using System.Threading;

namespace DynamicEvents
{
    class Publisher
    {
        public event Action _myEvent;
        private Timer _timer;

        public Publisher()
        {
            _timer = new Timer(RaiseEvent, null, 1000, 1000);
        }

        private void RaiseEvent(object dummy)
        {
            if (_myEvent != null)
            {
                _myEvent();
            }        
        }
    }
}
