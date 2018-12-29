using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    interface I
    {
        void G();
    }

    class C : I
    {
        public void G() { }
    }

    class D
    {
        public void H() { }
    }

    class Y : D, I
    {
        public void G()
        {
            throw new NotImplementedException();
        }
    }

    class E<T>
        where T: D, I
    {
        public static void J(T t1)
        {
            t1.G();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            D d1 = new D();
            // F2(d1);
            C c1 = new C();
            F2(c1);

            E<Y> e1 = new E<Y>();
            // E<D> e2 = new E<D>();

            List<int> l = new List<int>();
        }

        static void F<T>(T t)
        {
            string s = t.ToString();
        }

        static void F2<T>(T t)
            where T:I
        {
            t.G();
        }
    }
}
