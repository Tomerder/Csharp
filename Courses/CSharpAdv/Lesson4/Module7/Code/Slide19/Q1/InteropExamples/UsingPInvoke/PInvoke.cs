using System;
using System.Runtime.InteropServices;
using System.Text;

namespace UsingPInvoke
{
    class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        [DllImport(NATIVE_LIB_PATH, CharSet = CharSet.Ansi)]
        static extern void FillString(string text, char fill);

        static void Main(string[] args)
        {
            string s1 = "Amir";
            string s2 = s1;           
            //Marshaling a mutable string using StringBuilder:
            // StringBuilder text = new StringBuilder("Hello");
            FillString(s1, 'a');
            Console.WriteLine(s1);
        }
    }
}
