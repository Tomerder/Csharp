using System.Runtime.InteropServices;

namespace CallingApiFunctions
{
    public class PlatformInvokeTest
    {
        [DllImport("msvcrt.dll")]
        public static extern int puts(string c);
        [DllImport("msvcrt.dll")]
        internal static extern int _flushall();
    }

    class Program
    {
        static void Main(string[] args)
        {
            PlatformInvokeTest.puts("Hello World!");
            PlatformInvokeTest._flushall();
        }
    }
}
