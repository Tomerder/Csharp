using System;

namespace ReferenceTypesValueTypes
{
    // This is a declaration of a class, which is implicitly
    //  a reference type.
    //
    class ReferenceType
    {
        // This is the class' constructor.  It accepts
        //  a parameter of type 'int' (32-bit signed integer).
        //
        public ReferenceType(int value)
        {
            Value = value;
        }
        // This is a public field exposed by the class.
        //  It's similar to the C++ 'data member' concept.
        //
        public int Value;
    }

    class TypesDemo
    {
        // This method accepts a reference type parameter
        //  and attempts to modify it's 'Value' field.
        //
        static void ReferenceFunc(ReferenceType instance)
        {
            instance.Value = 9;
        }

        // This method accepts a value type parameter and
        //  attempts to modify it.
        //
        static void ValueFunc(int instance)
        {
            instance = 42;
        }

        static void Main(string[] args)
        {
            // The following section creates an instance of
            //  the reference type ReferenceType and the
            //  value type int (System.Int32).
            //
            ReferenceType obj1 = new ReferenceType(42);
            int i1 = 8;
            Console.WriteLine("ref: {0}  val: {1}", obj1.Value, i1);

            // Does modifying i2 modify i1?
            //
            int i2 = i1;
            i2 += 5;
            Console.WriteLine("i1: {0}  i2: {1}", i1, i2);

            // Does modifying obj2.Value modify obj1.Value?
            //
            ReferenceType obj2 = obj1;
            obj2.Value += 5;
            Console.WriteLine("obj1: {0}  obj2: {1}", obj1.Value, obj2.Value);

            ReferenceType obj3 = new ReferenceType(14);
            // Equality for reference types is defined as
            //  equality of references.  I.e., two variables
            //  of a reference type will be equal if and only
            //  if they point to the same instance.
            //
            if (obj3 == obj1)
            {
                Console.WriteLine("obj3 and obj1 ARE EQUAL!");
            }
            else
            {
                Console.WriteLine("obj3 and obj1 ARE NOT EQUAL!");
            }

            // Note that this line does not copy the data from
            //  obj2 to obj3.  It makes obj3 another reference
            //  to the obj2 object, meaning that the original
            //  object obj3 was referencing can now be collected
            //  by the .NET Garbage Collector.
            //
            obj3 = obj2;
        }
    }
}
