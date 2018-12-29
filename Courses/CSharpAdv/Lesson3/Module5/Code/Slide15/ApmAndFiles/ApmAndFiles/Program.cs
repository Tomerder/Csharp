using System;
using System.IO;

namespace ApmAndFiles
{
    /// <summary>
    /// Demonstrates a use of the .NET Asynchronous Programming Model with files
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CreateLargeFile();
            APMWithFiles();
        }

        /// <summary>
        /// Creates a large file for demonstration purposes.
        /// </summary>
        private static void CreateLargeFile()
        {
            using (StreamWriter writer = new StreamWriter("sample.txt"))
            {
                for (int i = 0; i < 5000000; ++i)
                {
                    writer.WriteLine(i);
                }
            }
        }

        /// <summary>
        /// Demonstrates the use of the APM with files, through the FileStream class.
        /// This method performs asynchronous reads and writes to copy data from an input
        /// file to an output file.  Reads and writes are interlaced, and proceed in chunks
        /// of 8KB at a time (displaying progress to the console).
        /// </summary>
        static void APMWithFiles()
        {
            FileStream reader = new FileStream("sample.txt", FileMode.Open);
            FileStream writer = new FileStream("sample2.txt", FileMode.Create);
            byte[] buffer1 = new byte[8192], buffer2 = new byte[8192];
            IAsyncResult ar1, ar2 = null;
            while (true)
            {
                ar1 = reader.BeginRead(buffer1, 0, buffer1.Length, null, null);
                while (!ar1.IsCompleted)
                {
                    Console.Write("R");
                }
                if (ar2 != null)
                {
                    while (!ar2.IsCompleted)
                    {
                        Console.Write("W");
                    }
                }
                int bytesRead;
                if ((bytesRead = reader.EndRead(ar1)) == 0)
                    break;  //No more data to read
                if (ar2 != null)
                {
                    writer.EndWrite(ar2);
                }
                Array.Copy(buffer1, buffer2, bytesRead);
                ar2 = writer.BeginWrite(buffer2, 0, bytesRead, null, null);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
