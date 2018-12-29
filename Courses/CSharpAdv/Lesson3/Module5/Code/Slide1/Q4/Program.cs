using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4
{
    delegate void Del1(int i);
    class Program
    {
        static void Main(string[] args)
        {
            Del1 d1 = new Del1(F);
            Action<int> d2 = new Action<int>(F);
            Action<int, string> d3 = new Action<int, string>(F2);
        }

        static void F(int i) { }

        static void F2(int i, string s) { }
    }
}
