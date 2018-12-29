using System;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = new string[3];
            data[0] = "This is line 1";
            data[1] = "This is line 2";
            data[2] = "This is line 3";
            File.WriteAllLines("a.txt", data);

            foreach (string line in File.ReadAllLines("a.txt"))
            {
                Console.WriteLine(line);
            }
        }
    }
}
