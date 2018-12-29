//
//Non-paged CLR Host
//
//	By	Sasha Goldshtein - http://blogs.microsoft.co.il/blogs/sasha
//	and Alon Fliess		 - http://blogs.microsoft.co.il/blogs/alon
//
//All rights reserved (2008).  When incorporating any significant portion of the
//code into your own project, you must retain this copyright notice.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TestApplication
{
    class Program
    {
        public static void Main()
        {
            Main(1570000000.ToString());
        }

#pragma warning disable 0028
        public static int Main(string str)
#pragma warning restore 0028
        {
            long wsSize = long.Parse(str);
            Console.WriteLine("Received working set size: " + wsSize + ", press [RETURN] to start.");
            Console.ReadLine();

            const long ARRAY_SIZE = 4096;
            const float SAFETY_COEFF = 0.85f;
            const int ITERATIONS = 10;

            List<byte[]> arrays = new List<byte[]>();
            for (long i = 0; i < SAFETY_COEFF * wsSize / ARRAY_SIZE; ++i)
            {
                arrays.Add(new byte[ARRAY_SIZE]);
            }

            Console.WriteLine("Starting work...");

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < ITERATIONS; ++i)
            {
                Console.WriteLine("Outer iteration " + i);
                foreach (byte[] array in arrays)
                {
                    int sum = 0;
                    foreach (byte b in array)
                    {
                        sum += b;
                    }
                }
            }

            Console.WriteLine("Average time per iteration: " + sw.ElapsedMilliseconds / ITERATIONS + "ms");
            Console.WriteLine("Finished work, press [RETURN] to quit.");
            Console.ReadLine();

            return 0;
        }
    }
}
