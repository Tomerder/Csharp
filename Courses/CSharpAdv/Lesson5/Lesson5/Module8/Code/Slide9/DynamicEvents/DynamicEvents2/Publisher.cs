using System;
using System.Threading;

namespace DynamicEvents
{
    class Publisher
    {
        public static event Action _myEvent;
        private static Timer _timer;

        static Publisher()
        {
            _timer = new Timer(RaiseEvent, null, 1000, 1000);
        }

        private static void RaiseEvent(object dummy)
        {
            if (_myEvent != null)
            {
                _myEvent();
            }        
        }
    }
}
