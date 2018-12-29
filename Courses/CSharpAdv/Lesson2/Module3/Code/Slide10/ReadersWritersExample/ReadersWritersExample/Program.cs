using System;
using System.IO;

namespace ReadersWritersExample
{
    class Program
    {
        /// <summary>
        /// Demonstrates the use of StreamWriter and StreamReader
        /// to write and read textual information with files.
        /// </summary>
        static void Main(string[] args)
        {
            FileStream myFile = new FileStream("myfile.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(myFile);
            writer.WriteLine("From a stream writer!");
            writer.Close(); //This is critical!

            //Short-hand for creating a file
            StreamReader reader = new StreamReader("myfile.txt");
            Console.WriteLine(reader.ReadLine());
            reader.Close();
        }
    }
}
