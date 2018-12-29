using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    // delegate void Del();

    class Program
    {
        static void Main(string[] args)
        {
            Publisher p = new Publisher();
            p.MyDel += F;
            p.MyDel += G;
            p.Publish();
        }

        static void F(object sender, EventArgs e)
        {
            Console.WriteLine("F");
        }

        static void G(object sender, EventArgs e)
        {
            Console.WriteLine("G");
        }
    }

    class Publisher
    {
        public event EventHandler MyDel;

        public void Publish()
        {
            if (MyDel != null)
                MyDel(this, EventArgs.Empty);
        }
    }
}
