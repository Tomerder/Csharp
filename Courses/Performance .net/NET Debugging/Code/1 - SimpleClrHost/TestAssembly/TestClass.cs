using System;
using System.Collections.Generic;
using System.Text;

namespace TestAssembly
{
    public class TestClass
    {
        public static int MyFunc(string arg)
        {
            Console.WriteLine(arg);
            GC.Collect();
            return arg.Length;
        }
    }
}
