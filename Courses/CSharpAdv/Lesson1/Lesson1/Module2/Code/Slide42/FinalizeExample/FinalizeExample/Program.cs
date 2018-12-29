using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalizeExample
{
    class A
    {
        ~A()
        {
            Console.WriteLine("~A");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            F();
            GC.Collect();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void F()
        {
            A a1 = new A();
        }
    }
}
