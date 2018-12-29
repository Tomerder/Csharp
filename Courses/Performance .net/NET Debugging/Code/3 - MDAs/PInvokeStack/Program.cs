using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace PInvokeStack
{
    class Program
    {
        [DllImport("MyNativeDll", EntryPoint = "add")]
        static extern long Add(long first, long second);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool Beep(long freq, long duration);

        static void Main(string[] args)
        {
            Console.ReadLine();
            throw new ApplicationException();

            //Assembly.LoadFrom(@"D:\Development\NETDebugging\SmartBreakpoints\SmartBreakpoints\bin\Debug\SmartBreakpoints.exe");

            Beep(50, 500);

            long result = Add(5, 3);
            Console.WriteLine(result);
        }
    }
}
