using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fileNames = Directory.GetFiles("c:\\Amir");
            
            using (FileStream fs = File.Create("namesOfFiles.txt"))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (string fileName in fileNames)
                    {
                        sw.WriteLine(fileName);
                    }                
                }
            }
        }
    }
}
