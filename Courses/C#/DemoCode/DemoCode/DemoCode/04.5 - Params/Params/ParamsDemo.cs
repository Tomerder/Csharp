using System;
using System.Collections.Generic;
using System.Text;

namespace Params
{
    static class Util
    {
        // Note the use of the 'params' keyword when declaring
        //  the method.  This means that the user can pass any
        //  number of variables of type double to this method.
        //
        public static double Average(params double[] values)
        {
            // Note the use of the Array.Length property,
            //  which automatically holds the length of the array.
            //
            double sum = 0.0;
            for (int i = 0; i < values.Length; ++i)
            {
                sum += values[i];
            }
            return sum / values.Length;

            // The same code could be implemented with the
            //  foreach control statement, as follows:
            //
            //double sum = 0.0;
            //foreach (double value in values)
            //{
            //    sum += value;
            //}
            //return sum / values.Length;
        }
    }

    class A
    {
        static void Main()
        {
        }
    }

    class ParamsDemo
    {
        static void Main(string[] args)
        {
            // When calling the method, simply pass
            //  as many doubles as you want:
            //
            Console.WriteLine(Util.Average(5.2, 3.4, 1.1));

            // However, it can also accept a regular
            //  array of doubles:
            //
            double[] doubles = new double[] { 5.2, 3.4, 1.1 };
            Console.WriteLine(Util.Average(doubles));
        }
    }
}
