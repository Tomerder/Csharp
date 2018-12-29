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
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream);
            writer.WriteLine("From a stream writer!");
            writer.Close(); //This is critical!
        }
    }
}
