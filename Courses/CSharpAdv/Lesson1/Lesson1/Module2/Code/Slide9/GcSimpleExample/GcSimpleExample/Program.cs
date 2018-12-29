using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcSimpleExample
{
    class C
    {
        public int _a;
    }

    class Program
    {
        static void Main(string[] args)
        {
            C c1 = new C();
            C c2 = new C();
            C c3 = new C();
            c2 = null;
            // Here GC works...
        }
    }
}
