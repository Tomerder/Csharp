using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q8
{
    //delegate void Del();

    class Program
    {
        static void Main(string[] args)
        {
            //Del del = F;

            Action del = F;
            del();
        }

        static void F()
        {
            Console.WriteLine("F");
        }
    }
}
