using System;
using System.Runtime.InteropServices;

namespace UsingPInvoke
{
    unsafe class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        [DllImport(NATIVE_LIB_PATH)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsValid([MarshalAs(UnmanagedType.LPWStr)] string text);

        static void Main(string[] args)
        {            
            Console.WriteLine(IsValid(null));
            Console.WriteLine(IsValid("Hey!"));
        }
    }
}
