using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAttributesClasses
{
    public class TestClassAttributes : System.Attribute
    {
        public string TestName { get; set; }
    }

    public class TestMethodAttributes : System.Attribute
    {
        public string TestName { get; set; }
    }

}
