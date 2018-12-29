using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "A";
            string s2 = s.ToAmir(); // Amir
        }
    }

    static class StringExtensions
    {
        public static string ToAmir(this string s)
        {
            return "Amir";
        }
    }
}
