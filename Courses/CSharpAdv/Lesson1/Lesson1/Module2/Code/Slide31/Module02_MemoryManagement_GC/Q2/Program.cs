using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    delegate void Del();
    delegate void Del2(int i);

    class Program
    {
        static void Main(string[] args)
        {
            Publisher p = new Publisher();
            p.MyDel += F;
            p.MyDel += G;
            p.Publish();

            Del2 del2 = new Del2(F2);
        }

        static void F()
        {
            Console.WriteLine("F");
        }

        static void F2(int u)
        {
            Console.WriteLine("F");
        }


        static void G()
        {
            Console.WriteLine("G");
        }
    }

    class Publisher
    {
        public Del MyDel;

        public void Publish()
        {
            MyDel();
        }
    }
}
