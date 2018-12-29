using System;

namespace ParametersExample
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
            S s1 = new S();
            s1.b = 2;

            F(c1, s1);
            Console.WriteLine(c1.a);
            Console.WriteLine(s1.b);
        }

        private static void F(C c2, S s2)
        {
            c2.a = 3;
            s2.b = 4;
        }
    }
}
