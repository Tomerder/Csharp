using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOM1
{
    class OOM1Program
    {
        static void Main(string[] args)
        {
            for (int i = 2; ; i = (int)(i * 1.5))
            {
                byte[] b = new byte[i];
                Console.WriteLine("Processing data of size " + b.Length);
            }
        }
    }
}
