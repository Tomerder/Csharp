using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL
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
                //t.Wait();
            }

            Console.Read();
        }

        private static void PrintChar(object c)
        {
            Console.WriteLine((char)c);
        }

    }
}
