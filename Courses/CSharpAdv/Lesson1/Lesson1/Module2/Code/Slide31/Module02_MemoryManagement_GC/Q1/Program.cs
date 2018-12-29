using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    delegate void Del();

    class Program
    {
        static void Main(string[] args)
        {
            //Del del1 = F;
            //del1();

            Publisher p = new Publisher();
            p.MyDel += F;
            p.MyDel();
        }

        static void F()
        {
            Console.WriteLine("F");
        }
    }

    class Publisher
    {
        public Del MyDel;
    }
}
