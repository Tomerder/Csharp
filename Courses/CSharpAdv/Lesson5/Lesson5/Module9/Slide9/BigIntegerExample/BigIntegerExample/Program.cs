using System;
using System.Numerics;

namespace BigIntegerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 200;
            BigInteger result = new BigInteger(1);
            for (int i = 2; i <= n; ++i)
                result *= i;
            Console.WriteLine(result);
        }
    }
}
