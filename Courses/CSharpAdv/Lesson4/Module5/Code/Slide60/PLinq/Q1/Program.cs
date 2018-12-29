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
            List<int> l = new List<int>() { 8, 6, 4, 2 };

            IEnumerable<int> i = from n in l
                                 select n + 1; // 9 7 5 3


        }
    }
}
