using System;

namespace SystemObjectMethods
{
    class SystemObjectDemo
    {
        // We have already seen object.Equals (static and instance)
        //  and object.ReferenceEquals.  This demo shows some other
        //  methods.
        //
        static void Main(string[] args)
        {
            // object.GetHashCode should return a hash code for
            //  the object to be used in a hash table.
            //
            for (int i = 0; i < 10; ++i)
            {
                // int.GetHashCode simply returns the value.
                //
                Console.WriteLine(i.GetHashCode());
            }
            for (int i = 0; i < 10; ++i)
            {
                // object.GetHashCode attempts to return some
                //  good distribution.
                //
                Console.WriteLine((new object()).GetHashCode());
            }

            // object.GetType returns a System.Type object
            //  representing the type of the current instance.
            //
            int j = 5;
            Type typeOfInteger = j.GetType();
            // Some of the System.Type methods.  This will be
            //  further discussed in the section on Reflection.
            //
            Console.WriteLine("Full name: " + typeOfInteger.FullName);
            Console.WriteLine("IsPrimitive: " + typeOfInteger.IsPrimitive);
            Console.WriteLine("Base type: " + typeOfInteger.BaseType);
            // Note that typeof(int) is the same as j.GetType().

            // object.ToString returns a string representation
            //  of the instance.  The default implementation
            //  simply returns the type name, but some types
            //  override this implementation.
            //
            string s = "Hello";
            int k = 5;
            object o = new object();
            Console.WriteLine(s.ToString());
            Console.WriteLine(k.ToString());
            Console.WriteLine(o.ToString());
            // Note that Console.WriteLine knows that it should
            //  invoke object.ToString, so we could omit the
            //  .ToString() part in the previous three lines.

            string input = Console.ReadLine();
            int input2 = Convert.ToInt32(input);
        }
    }
}
