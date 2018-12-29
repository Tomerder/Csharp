using System;
using System.Collections.Generic;
using System.Text;

namespace ProfilerDemo
{
    public static class PrimeUtil
    {
        public static bool IsPrime(uint number)
        {
            if (number == 0 || number == 1)
                return false;

            for (int i = 2; i < (int)Math.Ceiling(Math.Sqrt(number)); ++i)
                if (number % i == 0) return false;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            for (uint i = 0; i < 100000; ++i)
                if (PrimeUtil.IsPrime(i)) Console.WriteLine(i);
            Console.ReadLine();

        }
    }
}
