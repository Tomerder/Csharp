using System;

namespace OperatorOverloading
{
    class OperatorOverloadingDemo
    {
        static void Main(string[] args)
        {
            Rational r1 = new Rational(2, 5);
            Rational r2 = new Rational(1, 7);

            Console.WriteLine(r1 == r2);
            Console.WriteLine(r1 > r2);

            r1 += 5;
            Console.WriteLine(r1.ToString());

            r2++;
            Console.WriteLine(r2.ToString());

            int i = (int)r1;    // This wouldn't compile without the cast.
            double d = r2;
        }
    }
}
