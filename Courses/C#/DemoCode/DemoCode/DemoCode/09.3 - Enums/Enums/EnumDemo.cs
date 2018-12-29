using System;

namespace Enums
{
    // The enum definition is very similar to the C++ syntax.
    //  However, in C++ an enum is merely an integer constant.
    //  In C#, it's a full-blown value-type, derived from System.Enum,
    //  that provides various services.  The default enum is
    //  Int32-based (meaning that is occupies 4 bytes of memory),
    //  but that can be changed, e.g.:
    //
    //      enum Fruit : byte
    //      enum Fruit : ushort
    //
    //  and so forth.  Note that the enum fields' default values
    //  start from 0, but values can be explicitly provided,
    //  just like in C++.
    //
    enum Fruit
    {
        Apple,
        Banana,
        Orange
    }

    // The following is an enum where each value represents
    //  a bit in a bitmask.  It allows us to combine various
    //  values in a single variable.  Such an enum must be
    //  marked with the [Flags] attribute.  It is also advised
    //  that any enum, especially a bitmask enum, to have
    //  a None value with the value 0.
    //
    [Flags]
    enum AvailableComputerParts
    {
        None                    = 0x0, // This is a best-practice.
        Cpu                     = 0x1,
        Ram                     = 0x2,
        HardDrive               = 0x4,
        Motherboard             = 0x8,
        NetworkAdapter          = 0x10,
        Scsi                    = 0x1f
    }

    class EnumDemo
    {
        static void Main(string[] args)
        {
            #region Regular Enum

		    // Unlike C++, in C# an enum value must be qualified
            //  with the enum name.  As far as the compiler is
            //  concerned, an enum definition is quite similar to
            //  the following:
            //
            //      struct Fruit : System.Enum
            //      {
            //          public const int Apple = 0;
            //          public const int Banana = 1;
            //          public const int Orange = 2;
            //      }
            //
            Fruit fruit = Fruit.Banana;

            // Fruit has an override of the Object.ToString method
            //  that correctly prints the enum value.
            //
            Console.WriteLine("The fruit is: {0}", fruit);

            // Enum.GetNames is a static method that provides us the
            //  string values of the enum.  It's useful to display
            //  the various options.
            //
            string[] fruitNames = Enum.GetNames(typeof(Fruit));
            foreach (string fruitName in fruitNames)
            {
                Console.WriteLine(fruitName);
            }

            Console.Write("Enter your fruit name: ");
            string myFruitName = Console.ReadLine();
            try
            {
                // Enum.Parse is a static method that can parse a fruit
                //  name and convert it to an actual Fruit instance.
                //  It throws an ArgumentException exception if it fails,
                //  so it's best practice to catch that exception.
                //
                Fruit myFruit = (Fruit)Enum.Parse(typeof(Fruit), myFruitName);
                Console.WriteLine("Correctly parsed your choice: " + myFruit);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine("Invalid fruit: " + exception.ToString());
            }

            // Finally, we can check if some integral value is defined
            //  within the specified enum, and create the enum instance
            //  if it is.
            //
            Console.Write("Enter a numeric value: ");
            int value = Convert.ToInt32(Console.ReadLine());
            if (Enum.IsDefined(typeof(Fruit), value))
            {
                Console.WriteLine("{0} is defined, and corresponds to {1}",
                                  value, Enum.ToObject(typeof(Fruit), value));
            }
            else
            {
                Console.WriteLine("{0} is not defined", value);
            }

	        #endregion        

            #region [Flags] Enum

            // To initialize a bitmask enum, we can use the bitwise OR operator.
            //
            AvailableComputerParts computerParts;
            computerParts = AvailableComputerParts.Cpu | AvailableComputerParts.Ram;
            computerParts |= AvailableComputerParts.NetworkAdapter;

            // To test for a given value, we can use the bitwise AND operator.
            //
            if ((computerParts & AvailableComputerParts.Scsi) != 0)
            {
                Console.WriteLine("A SCSI is available");
            }

            // Finally, we can print the enum, and the [Flags] attribute
            //  indicates that a list of values may be printed:
            //
            Console.WriteLine(computerParts);

            #endregion
        }
    }
}
