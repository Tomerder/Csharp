using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    delegate void MyDel(int i);

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                // F(i);
                MyDel myDel = new MyDel(F);
                myDel(i);
            }
        }

        private static void F(int i)
        {
            Console.WriteLine(i);
        }
    }
}
