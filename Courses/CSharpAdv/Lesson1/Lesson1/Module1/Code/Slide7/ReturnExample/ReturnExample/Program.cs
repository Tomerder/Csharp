namespace ReturnExample
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
            C c1 = F1();
            S s1 = F2();
        }

        private static C F1()
        {
            C c2 = new C();
            c2.a = 1;
            return c2;
        }

        private static S F2()
        {
            S s2 = new S();
            s2.b = 2;
            return s2;
        }
    }
}
