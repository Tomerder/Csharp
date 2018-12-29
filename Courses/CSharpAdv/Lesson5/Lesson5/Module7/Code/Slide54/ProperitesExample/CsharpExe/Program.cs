using CliDllNamespace;
using System;

namespace CsharpExe
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex complex = new Complex(2.0, 3.0);
            Console.WriteLine("{0} + {1}i",
		complex.Real, complex.Imaginary);
	        Console.WriteLine("= {0:F2}*(cos({1:F2}) + i*sin({1:F2}))",
		complex.R, complex.Theta);
        }
    }
}
