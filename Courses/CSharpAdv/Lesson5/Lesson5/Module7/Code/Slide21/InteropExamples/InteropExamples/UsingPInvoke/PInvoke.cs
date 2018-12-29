using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UsingPInvoke
{
    class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        delegate bool IsMatch(string text);

        [DllImport(NATIVE_LIB_PATH, CharSet = CharSet.Ansi)]
        static extern int FindMatch(IsMatch match);

        static void Main(string[] args)
        {            
            //Marshaling delegates with an anonymous method:
            IsMatch match = delegate(string s) { return s.StartsWith("Go"); };
            Console.WriteLine(FindMatch(match));

            //Marshaling delegates with a lambda expression:
            match = s => s.StartsWith("I");
            Console.WriteLine(FindMatch(match));
        }
    }
}
