using CliDll;
using System;

namespace CsharpExe
{
    class Program
    {
        static void Main(string[] args)
        {
            R r = new T();
            Console.WriteLine("{0} {1} {2} {3} {4} {5}", r.c(), r.d(), r.f(), r.x(), r.y(), r.z());

            I1 i1 = r;
            Console.WriteLine("{0} {1} {2} {3}", i1.c(), i1.d(), i1.f(), i1.h());
        }
    }
}
