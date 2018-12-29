using System;

namespace Events
{

    public class TV
    {
        public TV(Switch theSwitch)
        {
            _mSwitch = theSwitch;
            _mSwitch.SwitchFlipped += OnSwitchFlipped;
        }
        public static void OnSwitchFlipped(object sender, SwitchFlippedEventArgs args)
        {
            Console.WriteLine("This is the TV, switch flipped to state: " + args.State);
        }
        private Switch _mSwitch;
    }
    public class Light
    {
        public Light(Switch theSwitch)
        {
            _mSwitch = theSwitch;
            _mSwitch.SwitchFlipped += OnSwitchFlipped;
        }
        public static void OnSwitchFlipped(object sender, SwitchFlippedEventArgs args)
        {
            Console.WriteLine("This is the Light, switch flipped to state: " + args.State);
        }
        private Switch _mSwitch;
    }
    class EventsDemo
    {
        static void Main(string[] args)
        {
            Switch @switch = new Switch();
            TV tv1 = new TV(@switch);
            Light light1 = new Light(@switch);

            // The following line registers to the SwitchFlipped
            //  event of the @switch object.  The OnSwitchFlipped
            //  method will be called whenever the switch is flipped.
            //
            //@switch.SwitchFlipped += OnSwitchFlipped;
            //@switch.SwitchFlipped += OnSwitchFlipped2;


            @switch.Flip();
            @switch.Flip();
            @switch.Flip();
        }

        //static void OnSwitchFlipped(object sender, SwitchFlippedEventArgs args)
        //{
        //    Console.WriteLine("Switch flipped to state: " + args.State);
        //}

        //static void OnSwitchFlipped2(object sender, SwitchFlippedEventArgs args)
        //{
        //    Console.WriteLine("Switch flipped to state: vvvvvvvvvvvv" + args.State);
        //}
    }
}
