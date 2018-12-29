using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4
{
    class C
    { }

    class D1: C
    {

    }

    class D2 : C { }

    class Program
    {
        static void Main(string[] args)
        {
            C c1 = new D1();
            D1 d1 = (D1)c1;
            // D2 d2 = (D2)c1;
            D2 d2 = c1 as D2;
            if (d2 != null)
            {
                //...
            }
        }
    }
}
