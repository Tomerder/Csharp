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
            string s = "Amir";
            F(s);
            Console.WriteLine(s);
            G(ref s);
            Console.WriteLine(s);
        }

        private static void F(string s)
        {
            s = "Adler";
        }

        private static void G(ref string s)
        {
            s = "Adler";
        }
    }
}
