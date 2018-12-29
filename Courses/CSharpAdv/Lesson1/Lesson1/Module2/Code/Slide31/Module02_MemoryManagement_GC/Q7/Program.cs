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
            Publisher p = new Publisher();
            p.MyDel += F;
            p.MyDel += G;
            p.Publish();
        }

        static void F(object sender, MyEventArgs e)
        {
            int i = e.I;
            Console.WriteLine("F");
        }

        static void G(object sender, MyEventArgs e)
        {
            Console.WriteLine("G");
        }
    }

    class Publisher
    {
        public event EventHandler<MyEventArgs> MyDel;

        public void Publish()
        {
            if (MyDel != null)
            {
                MyDel(this, new MyEventArgs() { I = 5 });
            }
                
        }
    }

    class MyEventArgs: EventArgs
    {
        public int I { get; set; }
    }
}
