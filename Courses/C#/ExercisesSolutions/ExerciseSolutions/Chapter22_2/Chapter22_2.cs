using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Chapter22_2
{
    class Program
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        delegate bool EnumWindowsProc(IntPtr hwnd, int dummy);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(EnumWindowsProc callback, int dummy);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int maxCount);

        static void Main(string[] args)
        {
            EnumWindowsProc proc = delegate(IntPtr hwnd, int dummy)
            {
                StringBuilder text = new StringBuilder(256);
                GetWindowText(hwnd, text, text.Capacity);
                Console.WriteLine(text.ToString());
                return true;
            };

            EnumWindows(proc, 0);
            
            GC.KeepAlive(proc);
        }
    }
}
