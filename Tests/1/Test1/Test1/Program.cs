using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string str in args)
            {
                Console.WriteLine(str);
                //MessageBox.Show(str);
            }

            Console.ReadLine();
        }
    }
}
