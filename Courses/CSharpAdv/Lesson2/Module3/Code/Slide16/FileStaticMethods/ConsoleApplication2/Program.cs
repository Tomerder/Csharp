using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            F1();
            F2();
            F3();
            F4();
            F5();
        }

        static void F1()
        {
            string[] data = new string[3];
            data[0] = "a";
            data[1] = "b";
            data[2] = "c";
            File.WriteAllLines("a.txt", data);
        }

        static void F2()
        {
            string[] data = new string[2];
            data[0] = "d";
            data[1] = "e";
            File.AppendAllLines("a.txt", data);
        }

        private static void F3()
        {
            string filePath = "a.txt";
            string fileContents = "I am writing this text to a file called a.txt";
            File.AppendAllText(filePath, fileContents);
        }

        static void F4()
        {
            string filePath = "myFile.txt";
            byte[] fileBytes = { 12, 134, 12, 8, 32 };
            File.WriteAllBytes(filePath, fileBytes);
        }

        static void F5()
        {
            byte[] fileBytes = File.ReadAllBytes("myFile.txt");
        }
    }
}
