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
            string s1 = "Amir";
            string s2 = s1;
            s1 += " Adler";
            Console.WriteLine(s2); // Amir
        }
    }
}
