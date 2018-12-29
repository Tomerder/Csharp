using System;

namespace InheritanceSubtleties
{
    class InheritanceSubtletiesDemo
    {
        static void Main(string[] args)
        {
            // The following code demonstrates the behaviour
            //  described in the VirtualMethodSubleties.cs file.
            //
            VirtualMethods.Base b = new VirtualMethods.Derived();

            // The following code demonstrates the behaviour
            //  described in the OverloadingSubtleties.cs file.
            //
            Overloading.Derived d = new Overloading.Derived();
            d.Foo1(5);
            d.Foo1(5.5);
            d.Foo2(1);
            d.Foo2("Hello");
        }
    }
}
