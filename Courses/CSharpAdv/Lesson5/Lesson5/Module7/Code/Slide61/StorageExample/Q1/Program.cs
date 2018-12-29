using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class C
    {
        ~C() { Console.WriteLine("~C"); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            C c1 = new C();
        }
    }
}
