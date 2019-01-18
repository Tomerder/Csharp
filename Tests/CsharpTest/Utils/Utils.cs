using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLib
{
    static public class Utils
    {
        public static void MeasureIters(Func<int> _func, int _iters)
        {
            int result = _func();//Warm up - for JIT translation + Virtual memory (paging) + cache  
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < _iters; ++i)
            {
                result = _func();
            }
            Console.WriteLine(_func.Method.ToString() + " : " + sw.ElapsedMilliseconds);
        }

        public static void Measure(Func<long> _func)
        {
         
            Stopwatch sw = Stopwatch.StartNew();
            long result = _func();
            Console.WriteLine(_func.Method.ToString() + " : " + sw.ElapsedMilliseconds + " *** result = " + result);
        }
    }
}
