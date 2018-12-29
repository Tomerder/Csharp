using System;

namespace InheritanceSubtleties.Overloading
{
    static class Util
    {
        // This utility method shows the method name (type + name) of
        //  the method that called it.  It uses the System.Diagnostics
        //  StackTrace and StackFrame classes, and the System.Reflection
        //  MethodBase class.
        //
        static public void ShowCallerName()
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(1);
            System.Reflection.MethodBase method = stackFrame.GetMethod();
            Console.WriteLine(method.DeclaringType.Name + "." + method.Name);
        }
    }

    class Base
    {
        public void Foo1(double d) { Util.ShowCallerName(); }
        public void Foo2(string s) { Util.ShowCallerName(); }
    }

    // The Derived class appears to overload base class methods.
    // In C++, derived class methods hide base class methods BY NAME.
    //  This means that the base class methods would not even participate
    //  in the method resolution, even though the signatures are different.
    // In C#, derived class methods hide base class methods BY SIGNATURE.
    //  This means that since the method signatures of the derived class
    //  methods are not the same, both the base class and the derived class
    //  methods will participate in the method resolution, correctly mapping
    //  to the calls we expect.
    //
    class Derived : Base
    {
        public void Foo1(int i) { Util.ShowCallerName(); }
        public void Foo2(int i) { Util.ShowCallerName(); }
    }
}