using System;

namespace AssignExample
{
    class C
    {
        public int a;
    }

    struct S
    {
        public int b;
    }

    class Program
    {
        static void Main(string[] args)
        {
            C c1 = new C();
            c1.a = 1;
            C c2 = c1;
            c2.a = 2;
            Console.WriteLine("{0} {1}", c1.a, c2.a);

            S s1 = new S();
            s1.b = 3;
            S s2 = s1;
            s2.b = 4;
            Console.WriteLine("{0} {1}", s1.b, s2.b);
        }
    }
}
