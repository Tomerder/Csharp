using System;

namespace ConsoleApplication1
{
    class A
    {
        ~A()
        {
            Console.WriteLine("Finalize called...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(1);
            A a = new A();
            Console.WriteLine(2);
            Console.ReadLine();
            GC.Collect();
            Console.WriteLine(3);
            Console.ReadLine();
            a = null;
            Console.WriteLine(4);
            Console.ReadLine();
            GC.Collect();
            Console.WriteLine(5);
            Console.ReadLine();
            Console.WriteLine(6);
        }
    }
}
