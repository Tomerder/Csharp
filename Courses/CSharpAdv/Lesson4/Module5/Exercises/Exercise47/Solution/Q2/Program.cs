using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "abcdefgh";

            foreach (char c in s.ToCharArray())
            {
                Task t = new Task(() => PrintChar(c, 5));
                t.Start();
            }

            Console.Read();
        }

        private static void PrintChar(char c, int d)
        {
            Console.WriteLine(c);
        }
    }
}
