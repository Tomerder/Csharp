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
                Task t = new Task(o => Console.WriteLine(o), c);
                t.Start();
            }

            Console.Read();
        }
    }
}
