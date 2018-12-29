using System;

namespace InheritanceSubtleties.VirtualMethods
{
    class Base
    {
        // The Base class constructor calls the virtual Foo method.
        // In C++, during base class constructor activation, the
        //  object's vtable pointer points to the base class virtual
        //  table, meaning that the base class method will be invoked.
        // In C#, this call will be correctly forwarded (using the vtable)
        //  to the Derived.Foo method.  HOWEVER, the Derived class
        //  constructor has not been called yet!
        //
        public Base()
        {
            Console.WriteLine("Base ctor");
            Foo();
        }

        public virtual void Foo()
        {
            Console.WriteLine("Base");
        }
    }

    class Derived : Base
    {
        private int _i;

        public Derived()
        {
            // We will only see this printed AFTER the call
            //  to Derived.Foo, meaning that the constructor
            //  is called after some code in the class has 
            //  executed!
            //
            Console.WriteLine("Derived ctor");
            _i = 5000;
        }

        public override void Foo()
        {
            Console.WriteLine("Derived");
            // The following line will use the default value of
            //  the _i field, which happens to be 0.  If _i
            //  were a reference type that was supposed to be
            //  initialized in the constructor, a NullReferenceException
            //  might be thrown if we attempted to use it.
            Console.WriteLine("The value of _i = " + _i);
        }
    }
}