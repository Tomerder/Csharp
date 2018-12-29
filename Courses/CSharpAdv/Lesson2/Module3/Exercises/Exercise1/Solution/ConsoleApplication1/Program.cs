using System;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytesToWrite = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            WriteBytesToFile(bytesToWrite, "amir.bin");

            byte[] bytesToRead = new byte[4];
            ReadBytesFromFile(bytesToRead, "amir.bin", 4);

            foreach (byte b in bytesToRead)
            {
                Console.WriteLine(b);
            }
        }

        static void WriteBytesToFile(byte[] bytesToWrite, string nameOfFile)
        {
            FileStream fs = File.Create(nameOfFile);
            fs.Write(bytesToWrite, 0, bytesToWrite.Length);
            fs.Dispose();
        }

        static void ReadBytesFromFile(byte[] bytesToRead, string nameOfFile, int numOfBytes)
        {
            FileStream fs = File.Open(nameOfFile, FileMode.Open);
            fs.Read(bytesToRead, 0, numOfBytes);
            fs.Dispose();
        }
    }
}
