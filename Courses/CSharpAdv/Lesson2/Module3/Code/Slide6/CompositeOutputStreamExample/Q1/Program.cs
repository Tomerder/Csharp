using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            // F(1, 2, 3);
            int[] arr = { 1, 2, 3 };
            F(arr);

            G(1, 2, 3);
        }

        static void F(int[] arr)
        {
            foreach (int element in arr)
            {
                Console.WriteLine(element);
            }
        }

        static void G(params int[] arr)
        {
            foreach (int element in arr)
            {
                Console.WriteLine(element);
            }
        }
    }
}
