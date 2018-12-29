using System.Runtime.InteropServices;

namespace UsingPInvoke
{
    class PInvoke
    {
        const string NATIVE_LIB_PATH = @"..\..\..\..\..\Debug\NativeLibrary.dll";

        [DllImport(NATIVE_LIB_PATH, CharSet = CharSet.Unicode)]
        static extern void wputs(string s);

        static void Main(string[] args)
        {
            wputs("Amir");
        }
    }
}
