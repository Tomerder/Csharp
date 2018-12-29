using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point();
            p.X = 6.6;
            p.Y = 7.7;
            F(p);
        }

        private static void F(Point p)
        {
            Console.WriteLine($"X={p.X}, Y={p.Y}");
        }
    }
}
