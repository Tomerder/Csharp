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
                Task t = new Task(PrintChar, c);
                t.Start();
            }

            Console.Read();
        }

        private static void PrintChar(object o)
        {
            char c = (char)o;
            Console.WriteLine(c);
        }
    }
}
