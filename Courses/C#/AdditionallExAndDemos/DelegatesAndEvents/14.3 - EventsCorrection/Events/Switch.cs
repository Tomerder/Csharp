using System;

namespace Events
{
   
    // We define a class deriving from EventArgs that contains
    //  the state we want to pass along with the event.  This
    //  class holds the new switch state.
    //
    public class SwitchFlippedEventArgs : EventArgs
    {
        public SwitchFlippedEventArgs(SwitchState state)
        {
            _state = state;
        }

        public SwitchState State
        {
            get { return _state; }
        }

        private SwitchState _state;
    }

    public enum SwitchState
    {
        On,
        Off
    }

    // We define the delegate for the methods that can register to the
    //  event.  The event will be fired whenever the switch's state is
    //  changed (flipped).
    //
   public  delegate void SwitchFlippedEventHandler(object sender, SwitchFlippedEventArgs args);

    public class Switch
    {
        private SwitchState _state = SwitchState.Off;

        public void Flip()
        {
            if (_state == SwitchState.Off)
            {
                _state = SwitchState.On;
            }
            else
            {
                _state = SwitchState.Off;
            }
            // We call the protected method that raises the event.
            RaiseSwitchFlipped(new SwitchFlippedEventArgs(_state));
        }

        protected virtual void RaiseSwitchFlipped(SwitchFlippedEventArgs args)
        {
            // We must check that the event is not null.  It is null
            //  if no one registered to the event.
            //
            if (SwitchFlipped != null)
            {
                SwitchFlipped(this, args);
            }
        }

        // This is the definition of the event.  Note that the delegate
        //  appears in the event definition, so only methods that match
        //  the delegate signature can register to this event.
        //

       
        public event SwitchFlippedEventHandler SwitchFlipped;
    }
}
