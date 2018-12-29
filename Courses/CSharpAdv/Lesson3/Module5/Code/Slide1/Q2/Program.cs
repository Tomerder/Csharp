using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    delegate void Del(int i);

    class Program
    {
        static void Main(string[] args)
        {
            Del del1 = new Del(F);
            del1 += G;
            del1(4);
        }

        static void F(int i) { }

        static void G(int u) { }
    }
}
