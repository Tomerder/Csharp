using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder("a");
            F(sb);
            Console.WriteLine(sb.ToString());
            G(ref sb);
            Console.WriteLine(sb.ToString());
        }

        private static void F(StringBuilder sb)
        {
            sb = new StringBuilder("b");
        }

        private static void G(ref StringBuilder sb)
        {
            sb = new StringBuilder("b");
        }
    }
}
