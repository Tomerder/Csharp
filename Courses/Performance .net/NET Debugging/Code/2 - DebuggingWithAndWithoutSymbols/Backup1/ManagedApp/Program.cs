using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedApp
{
    class Program
    {
        static void Foo()
        {
            Bar();
        }
        static void Bar()
        {
            Baz();
        }
        static void Baz()
        {
            throw new ApplicationException("error");
        }

        static void Main(string[] args)
        {
            string s1 = "Hello World";
            string s2 = "Hello" + " World";

            Console.ReadLine();
            Foo();
        }
    }
}
