using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class C
    {
        public void F() { Console.WriteLine("F of C"); }
        public virtual void G() { Console.WriteLine("G of C"); }
        public virtual void H() { Console.WriteLine("H of C"); }
    }

    class D : C
    {
        public new void F() { Console.WriteLine("F of D"); }
        public override void G() { Console.WriteLine("G of D"); }
        public new void H() { Console.WriteLine("H of D"); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            C c1 = new C();
            c1.F();
            c1.G();
            c1.H();
            D d1 = new D();
            d1.F();
            d1.G();
            d1.H();
            C c2 = new D();
            c2.F();
            c2.G();
            c2.H();
        }
    }
}
