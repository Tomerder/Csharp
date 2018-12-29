using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q5
{
    delegate int Del1(string g, double n);

    class Program
    {
        static void Main(string[] args)
        {
            Del1 d1 = new Del1(F);
            int i = d1("a", 5.5);

            Func<string, double, int> d2 = new Func<string, double, int>(F);
            d2("a", 5.5);

            Func<C> d3 = new Func<C>(G);
            C c2 = d3();
        }

        static int F(string s, double d)
        {
            return 5;
        }

        static C G()
        {
            return new C();
        }
    }

    class C
    { }
}
