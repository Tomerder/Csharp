using System;
using System.Text;

namespace Strings
{
    class StringsDemo
    {
        static void MeasurePerformance()
        {
            // This isn't the best way to measure performance,
            //  but it will do just fine for our needs.  The
            //  Environment.TickCount property holds the number
            //  of milliseconds elapsed since Windows has started.
            // If you're interested in an alternative performance-
            //  measurement mechanism, you may consult the
            //  documentation of the System.Diagnostics.Stopwatch
            //  class, which is implemented using the Win32-API
            //  QueryPerformanceCounter.
            //
            int start = Environment.TickCount;
            StringManipUsingString();
            Console.WriteLine("System.String: {0} ms", Environment.TickCount - start);
            
            start = Environment.TickCount;
            StringManipUsingStringBuilder();
            Console.WriteLine("System.Text.StringBuilder: {0} ms", Environment.TickCount - start);

            // Sample results:
            //      System.String: 10563 ms
            //      System.Text.StringBuilder: 63 ms
            // (Pentium IV, 3.0 Ghz, 1.0 GB RAM)
        }

        // This method adds a value to a string for 100,000
        //  times.  This means that 100,001 instances are created.
        //
        static void StringManipUsingString()
        {
            string s = string.Empty;
            for (int i = 0; i < 100000; ++i)
            {
                s += Convert.ToChar(i % 256);
            }
        }

        // This method adds a value to a StringBuilder, defined
        //  in the System.Text namespace.  Only one instance
        //  of the StringBuilder is used throughout the method.
        //
        static void StringManipUsingStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100000; ++i)
            {
                sb.Append(Convert.ToChar(i % 256));
            }
        }

        static void Main(string[] args)
        {
            // String.Empty is the sole static instance of
            //  the empty string.  Note that 'string' is an
            //  alias for 'System.String', so we can use
            //  string, System.String and String interchangeably
            //  (the last one only if we have 'using System').
            //
            Console.WriteLine(String.Empty);

            // System.String's formatting facilities are very
            //  widely used.  Here's a sample (we've seen several
            //  others in one of the first demonstrations).
            //
            string s = string.Format("{0:C}", 5);
            
            // System.String has extensive lookup facilities,
            //  but if you're looking for regular expressions,
            //  you will find them in the System.Text.RegularExpressions
            //  namespace.  In their stead, you have:
            //
            Console.WriteLine("Hello World".StartsWith("Hello"));
            Console.WriteLine("Hello World".ToUpper());
            Console.WriteLine("Hello World".Trim('d', 'l'));
            Console.WriteLine(String.Join("#", "Hello World".Split(' ')));
            Console.WriteLine(String.IsInterned("Hello World") != null);
            // You are more than welcome to check out the MSDN documentation
            //  and read up further on the various System.String methods.
            //  For C/C++ programmers accustomed to strxxx/std::string,
            //  it will probably be a very enjoyable experience.

            MeasurePerformance();
        }

        // There are various ways to validate a string for
        //  emptiness.  Some examples:
        //
        static void ValidateString(string s)
        {
            // This is slightly more efficient
            //  than checking with String.Empty:
            //
            if (s == null || s.Length == 0)
            {
            }

            // This is probably the worst, performance-wise:
            //
            if (s == null || s == String.Empty)
            {
            }

            // But the best (and simplest) way is:
            //
            if (String.IsNullOrEmpty(s))
            {
            }
        }
    }
}
