using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3
{
    delegate void MyDel(int i);

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                // F(i);
                // MyDel myDel = new MyDel(F);
                // myDel(i);

                MyDel myDel = new MyDel(F);
                myDel.BeginInvoke(i, null, null);
            }

            Console.Read();
        }

        private static void F(int i)
        {
            Console.WriteLine(i);
        }
    }
}
