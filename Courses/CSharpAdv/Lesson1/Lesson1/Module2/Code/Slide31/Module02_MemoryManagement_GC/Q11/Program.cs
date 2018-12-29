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
            
            for (int i = a.GetInvocationList().Count() - 1; i >= 0; i--)
            {
                a.GetInvocationList()[i].DynamicInvoke();
            }
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
