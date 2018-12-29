using System;

namespace ComparingObjects
{
    class Cl { }

    class ComparingObjects
    {
        static void Main(string[] args)
        {
            Cl cl1 = new Cl();
            Cl cl2 = new Cl();
            int i = 5;
            int j = 5;
            object o = i;

            Console.WriteLine("reference type:");
            Console.WriteLine("cl1 == cl2 : {0}", cl1 == cl2);
            Console.WriteLine("cl1.Equals(cl2) : {0}", cl1.Equals(cl2));
            // Note: the static Equals method is inherited from object,
            //  so we can use it directly inside the ComparingObjects
            //  class which hosts our Main method.
            //
            Console.WriteLine("Equals(cl1, cl2) : {0}", Equals(cl1, cl2));
            Console.WriteLine("ReferenceEquals(cl1, cl2) : {0}", ReferenceEquals(cl1, cl2));

            Console.WriteLine("value type:");
            Console.WriteLine("i == j : {0}", i == j);
            Console.WriteLine("i.Equals(j) : {0}", i.Equals(j));
            Console.WriteLine("Equals(i, j) : {0}", Equals(i, j));
            Console.WriteLine("ReferenceEquals(i, j) : {0}", ReferenceEquals(i, j));

            Console.WriteLine("misc:");
            Console.WriteLine("ReferenceEquals(cl1, cl1) : {0}", ReferenceEquals(cl1, cl1));
            Console.WriteLine("ReferenceEquals(i, j) : {0}", ReferenceEquals(i, j));
            Console.WriteLine("ReferenceEquals(i, i) : {0}", ReferenceEquals(i, i));
            Console.WriteLine("ReferenceEquals(o, o) : {0}", ReferenceEquals(o, o));
        }
    }
}
