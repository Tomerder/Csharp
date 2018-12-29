using System;
using System.Runtime.InteropServices;
using System.Text;

namespace UsingPInvoke
{
    class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        [DllImport(NATIVE_LIB_PATH, CharSet = CharSet.Ansi)]
        static extern void FillString(StringBuilder text, char fill);

        static void Main(string[] args)
        {            
            //Marshaling a mutable string using StringBuilder:
            StringBuilder text = new StringBuilder("Hello");
            FillString(text, 'a');
            Console.WriteLine(text);
        }
    }
}
