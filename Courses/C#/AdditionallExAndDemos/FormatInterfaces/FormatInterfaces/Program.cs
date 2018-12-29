using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormatInterfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter Person Name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter Person Age");
            string age = Console.ReadLine();
            int iAge = int.Parse(age);
            Person p = new Person(name,iAge);
            Console.WriteLine("Please enter format to print person");
            string format = Console.ReadLine();
            string toPrint = "The person details in your format\n" + "{0:" + format + "}" ;
            Console.WriteLine(toPrint, p);
        }
    }
}
