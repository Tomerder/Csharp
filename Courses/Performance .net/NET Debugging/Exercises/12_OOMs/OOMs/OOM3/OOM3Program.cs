using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace OOM3
{
    class OOM3Program
    {
        static void Main(string[] args)
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; ; ++i)
            {
                Marshal.AllocHGlobal(1048576);
                byte[] data = new byte[1048576];
                list.Add(data);
                Thread.Sleep(10);
                Console.WriteLine("Processed another buffer...");
            }
        }
    }
}
