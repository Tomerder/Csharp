using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MeasuringMemcpy
{
    class Program
    {
        [DllImport("msvcrt")]
        static extern void memcpy(byte[] first, byte[] second, int size);

        static void Measure(Action act, string description)
        {
            act(); //warmup
            Stopwatch sw = Stopwatch.StartNew();
            act();
            Console.WriteLine(description + " took " + sw.ElapsedMilliseconds + " ms");
        }

        static unsafe void MyCopy(byte[] first, byte[] second)
        {
            fixed (byte* p1 = &first[0])
            fixed (byte* p2 = &second[0])
            {
                long* ptr1 = (long*)p1;
                long* ptr2 = (long*)p2;
                int size = first.Length / sizeof(long);
                size /= 4;
                for (int i = 0; i < size; ++i)
                {
                    *ptr2 = *ptr1;
                    ++ptr1;
                    ++ptr2;
                    *ptr2 = *ptr1;
                    ++ptr1;
                    ++ptr2;
                    *ptr2 = *ptr1;
                    ++ptr1;
                    ++ptr2;
                    *ptr2 = *ptr1;
                    ++ptr1;
                    ++ptr2;
                }
            }
        }

        static void Main(string[] args)
        {
            for (int size = 16; size <= 2*1048576; size *= 2)
            {
                byte[] first = (from n in Enumerable.Range(0, size) select (byte)(n % 256)).ToArray();
                byte[] second = (from n in Enumerable.Range(0, size) select (byte)(n % 256)).ToArray();
                for (int i = 0; i < 4; ++i)
                {
                    Measure(() =>
                        {
                            for (int j = 0; j < 1000; ++j)
                            {
                                Array.Copy(first, second, first.Length);
                            }
                        },
                        size + " Array.Copy");
                    Measure(() =>
                        {
                            for (int j = 0; j < 1000; ++j)
                            {
                                memcpy(first, second, first.Length);
                            }
                        },
                        size + " memcpy");
                    Measure(() =>
                        {
                            for (int j = 0; j < 1000; ++j)
                            {
                                MyCopy(first, second);
                            }
                        },
                        size + " MyCopy");
                }
            }
        }
    }
}
