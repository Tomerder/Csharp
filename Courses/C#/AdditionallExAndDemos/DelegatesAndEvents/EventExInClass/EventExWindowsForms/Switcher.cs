using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public enum SwitchState
    {
        On,
        Off
    }

    public delegate void SwitcherEventHandler(object sender, SwitchEventArgs e);

    public class Switcher
    {
        public SwitchState State { get; private set; }

        public event SwitcherEventHandler Switch;
        public event SwitcherEventHandler BeforeSwitch;

        public void DoSwitch()
        {

            OnBeforeSwitch();
            if (State == SwitchState.Off)
                State = SwitchState.On;
            else
                State = SwitchState.Off;

            OnSwitch();
        }

        protected virtual void OnBeforeSwitch()
        {
            if (BeforeSwitch != null)
            {
                BeforeSwitch.Invoke(this, new SwitchEventArgs(State));
            }
        }

        protected virtual void OnSwitch()
        {
            if (Switch != null)
            {
                Switch.Invoke(this, new SwitchEventArgs(State));
            }
        }

    }

    public class SwitchEventArgs : EventArgs
    {
        public SwitchState NewState { get; private set; }

        public SwitchEventArgs(SwitchState newState)
        {
            NewState = newState;
        }
    }




    public class ComputerSwitch : Switcher
    {
        protected override void OnSwitch()
        {


            base.OnSwitch();
        }
    }
}
