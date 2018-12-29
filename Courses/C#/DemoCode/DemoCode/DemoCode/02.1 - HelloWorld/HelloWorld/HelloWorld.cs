// The System namespace contains many useful classes,
//  and serves as the root of the namespace system
//  for the .NET Framework's Class Library.
//
using System;

// Our first class is declared inside a namespace,
//  called HelloWorld.  This means that the full
//  type name for our class is actually HelloWorld.HelloApplication.
//
namespace HelloWorld
{
    // This is our first class, that contains the Main
    //  method.  It is impossible to declare any
    //  method outside the scope of a class.
    //
    class HelloApplication
    {
        // This is the program's entry point.  Note that
        //  the function is static, meaning it can be
        //  called without creating an instance of its
        //  surrounding class.
        //
        private static void Main()
        {
            // The Console class contains methods and properties
            //  for interacting with the console (for input
            //  and output).  The WriteLine method (which has
            //  several overloads) writes a line of text to the
            //  screen, followed by a newline character sequence.
            //
            Console.WriteLine("Hello World!");
        }
    }
}
