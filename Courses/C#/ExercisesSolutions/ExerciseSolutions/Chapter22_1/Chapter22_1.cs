using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Chapter22_1
{
    class Program
    {
        [DllImport(@"..\..\..\Debug\GoodOldCFunctions.dll", CharSet = CharSet.Ansi)]
        static extern void StripWhiteSpaceAndPunctuation(StringBuilder text,
            out int nWhitespace, out int nPunctuation);

        static void Main(string[] args)
        {
            int whitespace, punctuation;
            Console.Write("Enter a string: ");
            StringBuilder text = new StringBuilder(Console.ReadLine());

            StripWhiteSpaceAndPunctuation(text, out whitespace, out punctuation);
            Console.WriteLine("Stripped: {0}", text.ToString());
            Console.WriteLine("Removed {0} whitespace and {1} punctuation characters",
                whitespace, punctuation);
        }
    }
}
