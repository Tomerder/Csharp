using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Profiler_Time
{
    class Program
    {
        static private bool IsPrime(int i)
        {
            if (i % 100 == 0)
            {
                byte[] b = new byte[10000];
            }

            for (int j = 2; j < i/2; ++j)
            {
                if (i % j == 0)
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            for (int i = 2; i < 1000000; ++i)
            {
                IsPrime(i);
            }
        }
    }
}
