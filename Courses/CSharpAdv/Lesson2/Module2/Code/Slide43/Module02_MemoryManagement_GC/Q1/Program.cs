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
            string s = "a";

            for (int i = 0; i < 1000; i++)
            {
                s += "a";
            }

            s = "a";
            StringBuilder sb = new StringBuilder(s);

            for (int i = 0; i < 1000; i++)
            {
                sb.Append("a");
            }

            s = sb.ToString();
        }
    }
}
