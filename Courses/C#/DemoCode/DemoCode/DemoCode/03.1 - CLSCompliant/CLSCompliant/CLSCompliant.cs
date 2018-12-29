using System;

// Using the [CLSCompliant] attribute tells the compiler
//  to check for non-CLS-compliant types used in the public
//  types exposed from this assembly.  Without this attribute,
//  the warning for the CLSCompliantProgram.PublicFunction
//  would not have appeared.
//
[assembly: CLSCompliant(true)]

namespace CLSCompliantApplication
{
    public class CLSCompliantProgram
    {
        // This line generates the following warning:
        //      warning CS3001: Argument type 'uint' is not CLS-compliant
        //  because the function is public and it is hosted
        //  inside a public type (class).  It has a parameter of
        //  type uint (unsigned 32-bit integer) which is not a
        //  CLS-compliant type.
        //
        public static void PublicFunction(uint i) { }

        // There's no problem associated with this function because
        //  it is private (can only be accessed from within the
        //  current class).
        //
        private static void InternalFunction(uint i) { }

        static void Main(string[] args)
        {
        }
    }
}
