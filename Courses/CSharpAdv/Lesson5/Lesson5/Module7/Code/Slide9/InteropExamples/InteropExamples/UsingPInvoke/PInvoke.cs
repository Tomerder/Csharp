using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace UsingPInvoke
{
    class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        [DllImport(NATIVE_LIB_PATH)]
        static extern bool IsPrime(int number);

        static void Main(string[] args)
        {
            bool b = IsPrime(4);
            Console.WriteLine(b); // false
            b = IsPrime(5); 
            Console.WriteLine(b); // true 
            
            Array.ForEach(Enumerable.Range(2, 100).Where(IsPrime).ToArray(), Console.WriteLine);
        }
    }
}
