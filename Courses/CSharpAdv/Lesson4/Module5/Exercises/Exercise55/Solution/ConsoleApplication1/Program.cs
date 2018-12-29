using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "abcdefgh";

            Parallel.ForEach(s.ToCharArray(), PrintChar);
        }

        private static void PrintChar(char c)
        {
            Console.WriteLine(c);
        }
    }
}
