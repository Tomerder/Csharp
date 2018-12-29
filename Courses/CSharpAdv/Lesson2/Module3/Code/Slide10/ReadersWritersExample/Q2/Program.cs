using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream myFile = new FileStream("myfile.txt", FileMode.Create);
            F(myFile);
            MemoryStream ms = new MemoryStream();
            F(ms);
        }

        static void F(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("From a stream writer!");
            writer.Close(); //This is critical!

        }
    }
}
