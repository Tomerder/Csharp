using System;
using System.Collections.Generic;
using System.Text;

namespace CLRProfilerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = String.Empty;
            for (int i = 0; i < 10000; ++i)
                result += "a";
        }
    }
}
