using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStreamExample
{
    class Program
    {
        /// <summary>
        /// Demonstrates the fundamental FileStream APIs, including
        /// the file creation modes, file access modes, file sharing modes
        /// and various methods and properties for reading and writing
        /// data.  This example uses the Encoding.Unicode object to
        /// encode and decode strings from their byte array representations.
        /// </summary>
        static void Main(string[] args)
        {
            FileStream myFile = new FileStream("myfile.txt", FileMode.Create, FileAccess.ReadWrite);
            string greeting = "Good afternoon";
            byte[] output = Encoding.Unicode.GetBytes(greeting);
            myFile.Write(output, 0, output.Length);
            myFile.Close();

            myFile = new FileStream("myfile.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] input = new byte[myFile.Length];
            myFile.Read(input, 0, input.Length);
            myFile.Close();
            Console.WriteLine("Read from file: " + Encoding.Unicode.GetString(input));
        }
    }
}
