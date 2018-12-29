using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter2_1
{
    class Program
    {
        private static string Prompt(string message)
        {
            Console.Write(message + ":\t");
            return Console.ReadLine();
        }

        private static void PrintInColor(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = oldColor;
        }

        static void Main(string[] args)
        {
            string name = Prompt("Enter your name");
            string company = Prompt("Enter your company");
            string location = Prompt("Enter your location");

            PrintInColor("Hello " + name, ConsoleColor.Green);
            PrintInColor("You work for " + company + " at " + location, ConsoleColor.White);

            double d = 0.94522;
            Console.WriteLine("Without precision: {0}", d);
            Console.WriteLine("With one decimal digit: {0:F1}", d);
            Console.WriteLine("Exponential: {0:E}", d);
            Console.WriteLine("Percent: {0:P}", d);
        }
    }
}
