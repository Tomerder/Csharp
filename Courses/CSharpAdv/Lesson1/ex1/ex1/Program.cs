using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            A a1 = new A() { I = 5 };  //object initializer
            B b1 = new B() { J = 8 };  //object initializer

            Console.WriteLine(a1); //calls Console.WriteLine(Object a1);
            Console.WriteLine(b1);
            A a2 = a1;
            a2.I = 4;
            Console.WriteLine(a1);
            B b2 = b1;
            b2.J = 7;
            Console.WriteLine(b1);
            F(a1);
            Console.WriteLine(a2);

            Console.ReadLine();
        }
        static public void F(A _a1)
        {
            _a1.I = 3;
        }
    }

    class A
    {
        public int I { get; set; } //automatic property

        public override string ToString()  //defined as virtual method on Object
        {
            return I.ToString();
        }
    }

    struct B
    {
        public int J { get; set; } //automatic property
        public override string ToString()
        {
            return J.ToString();
        }
    }



}
