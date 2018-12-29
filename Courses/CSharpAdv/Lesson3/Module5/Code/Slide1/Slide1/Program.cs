using System;

namespace Slide1
{
    public delegate void Del(int i);

    class Program
    {
        static void Main(string[] args)
        {
            F(0);

            Del del1 = new Del(F);
            del1(1);

            Del del2 = F;
            del2(2);

            Del del3 = delegate(int i) { Console.WriteLine("ann " + i); };
            del3(3);

            Del del4 = (i => { Console.WriteLine("lmbda " + i); });
            del4(4);

            Action<int> del5 = F;
            del5(5);

            Action<int, string> del6 = G;
            Func<string, double, int> del7 = H;
        }

        public static void F(int i)
        {
            Console.WriteLine("F " + i);
        }

        public static void G(int i, string s)
        { }

        public static int H(string s, double d)
        {
            return 2;
        }
    }
}
