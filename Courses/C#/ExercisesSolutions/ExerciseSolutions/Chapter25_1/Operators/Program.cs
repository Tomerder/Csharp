using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Sum(2, 4, 3));
            Console.WriteLine(Sum("a", "b", "c"));
            Console.WriteLine(Sum(DateTime.Now, TimeSpan.FromHours(3)));
           
        }

        static dynamic Sum(params dynamic[] arr)
        {
            dynamic res = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                res += arr[i];
            }

            return res;
        }
    }
}
