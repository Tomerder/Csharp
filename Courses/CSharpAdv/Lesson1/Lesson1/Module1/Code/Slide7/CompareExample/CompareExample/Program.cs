using System;

namespace CompareExample
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
            C c2 = new C();
            c2.a = 1;
            bool b1 = (c1.Equals(c2));
            Console.WriteLine(b1);
            
            S s1 = new S();
            s1.b = 2;
            S s2 = new S();
            s2.b = 2;
            bool b2 = (s1.Equals(s2));
            Console.WriteLine(b2);
        }
    }
}
