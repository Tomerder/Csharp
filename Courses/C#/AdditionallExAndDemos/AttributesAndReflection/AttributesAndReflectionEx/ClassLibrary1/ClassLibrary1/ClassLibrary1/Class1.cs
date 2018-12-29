using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestAttributesClasses;

namespace ClassLibrary1
{
   // [TestClassAttributes(
    [TestClassAttributes(TestName = "This Is test one")]
    public class Class1
    {
        [TestMethodAttributes(TestName = "Attribute for method func1")]
        public void Func1()
        {
            Console.WriteLine("Hello");
        }

        public void Func2()
        {
            Console.WriteLine("f2");
        }
    }
}
