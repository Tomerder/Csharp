using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q10
{
    class Program
    {
        static void Main(string[] args)
        {
            Action a = F;
            a += G;
            a();
        }

        static void F()
        {
            Console.WriteLine("F");
        }

        static void G()
        {
            Console.WriteLine("G");
        }
    }
}
