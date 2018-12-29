using System;
using System.IO;

namespace FileAndDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "..\\..\\..\\FolderToSearch";
            Console.WriteLine("Directories:");
            Console.WriteLine("------------");
            foreach (string dir in Directory.GetDirectories(path))
            {
                Console.WriteLine(dir);
            }

            Console.WriteLine();
            Console.WriteLine("File:");
            Console.WriteLine("-----");
            foreach (string file in Directory.GetFiles(path))
            {
                Console.WriteLine(file);
            }
        }
    }
}
