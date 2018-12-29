using System;

namespace Events
{
    class EventsDemo
    {
        static void Main(string[] args)
        {
            Switch @switch = new Switch();

            // The following line registers to the SwitchFlipped
            //  event of the @switch object.  The OnSwitchFlipped
            //  method will be called whenever the switch is flipped.
            //
            @switch.SwitchFlipped += OnSwitchFlipped;

            @switch.Flip();
            @switch.Flip();
        }

        static void OnSwitchFlipped(object sender, SwitchFlippedEventArgs args)
        {
            Console.WriteLine("Switch flipped to state: " + args.State);
        }
    }
}
