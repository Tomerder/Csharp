using System;

namespace ConsoleApplication1
{
    class A
    {
        public int I { get; set; }

        public override string ToString()
        {
            return I.ToString();
        }
    }

    struct B
    {
        public int J { get; set; }

        public override string ToString()
        {
            return J.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a1 = new A() { I = 5 };
            B b1 = new B() { J = 8 };
            Console.WriteLine(a1); 
            Console.WriteLine(b1);
            A a2 = a1;
            a2.I = 4;
            Console.WriteLine(a1);
            B b2 = b1;
            b2.J = 7;
            Console.WriteLine(b1);
            F(a1);
            Console.WriteLine(a2);
        }

        private static void F(A a1)
        {
            a1.I = 3;
        }
    }
}
