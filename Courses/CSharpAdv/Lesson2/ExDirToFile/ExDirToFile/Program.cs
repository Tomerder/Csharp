using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExDirToFile
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteDirFilesNamesToFile(@"c:\amir", @"c:\Output.txt");
        }

        static void WriteDirFilesNamesToFile(string _folder, string _outputFile)
        {
            using (StreamWriter writer = new StreamWriter(_outputFile))
            {
                foreach (string file in Directory.GetFiles(_folder))
                {
                    writer.WriteLine(Path.GetFileName(file));
                }
            }
        }
    }
}
