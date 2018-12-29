using CllDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpToCli
{
    class Program
    {
        static void Main(string[] args)
        {
            //class defined on CLI 
            Class1 c1 = new Class1();
            c1.F(); //C++ code
        }
    }
}
