using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    struct C { }

    class Program
    {
        static void Main(string[] args)
        {
            int i = 5;
            object o = i; // boxing

            int j = (int)o; // unboxing
            double d = 6.6; // boxing
            string s = "amir"; 

            F(i);
            F(d);
            F(s);
            F(9);

            C c1 = new C();
            Console.WriteLine(c1); // boxing

            ArrayList al = new ArrayList();
            al.Add(5);
            al.Add("amir");

            List<int> l = new List<int>();
            l.Add(5);
        }

        public static void F(object o)
        { }
    }
}
