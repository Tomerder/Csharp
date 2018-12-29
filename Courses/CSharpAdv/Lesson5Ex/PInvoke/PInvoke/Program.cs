using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PInvoke
{
    static class PlatformInvokeTest
    {
        //managed signutures
        [DllImport("msvcrt.dll")]
        public static extern int puts(string str);

        [DllImport("msvcrt.dll")]
        public static extern int _flushall();

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
