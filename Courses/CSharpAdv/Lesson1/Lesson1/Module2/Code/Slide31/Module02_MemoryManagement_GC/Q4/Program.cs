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
            Publisher p = new Publisher();
            p.MyDel += F;
            p.MyDel += G;
            p.Publish();
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

    class Publisher
    {
        public event Del MyDel;

        public void Publish()
        {
            if (MyDel != null)
                MyDel();
        }
    }
}
