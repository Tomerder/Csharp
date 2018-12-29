using System;
using GraphicsLib;
using System.IO;

namespace GraphicsTester
{
    class GraphicsTesterMain
    {
        static void Main(string[] args)
        {
            if (File.Exists("GraphicsLib.dll"))
            {
                Console.WriteLine("NOT GOOD!");
            }

            Rectangle rect = new Rectangle(3, 5);
            Console.WriteLine(rect.Area);
        }
    }
}
