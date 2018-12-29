using System;

namespace Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a");
            C.b = 8;
            Console.WriteLine("b");
            C.b = 9;
            Console.WriteLine("c");
            C.b++;
            Console.WriteLine("d");
            C c1 = new C();
            Console.WriteLine("e");
            C c2 = new C();
            Console.WriteLine("f");
        }
    }

    class C
    {
        public int a;

        public static int b;

        public C()
        {
            Console.WriteLine("instance ctor called");
        }

        static C()
        {
            Console.WriteLine("static ctor called");
        }
    }
}
