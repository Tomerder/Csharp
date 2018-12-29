using System;
using System.Globalization;

namespace ConsoleDemo
{
    class ConsoleDemo
    {
        static void Main(string[] args)
        {
            // The WriteLine method can be used to display various
            //  types of objects, including the 'generic' System.Object.
            //
            Console.WriteLine("Hello World!");
            Console.WriteLine(5);

            Console.Write("Please enter your name: ");
            // We declare a string variable here to keep the
            //  user's answer and display it immediately afterwards.
            //
            string name = Console.ReadLine();
            Console.WriteLine("Your name is " + name);

            Console.Write("Please enter your favorite fruit: ");
            string fruit = Console.ReadLine();
            Console.WriteLine("Hello {0}, who loves {1}s", name, fruit);

            Console.Write("Please enter your salary: ");
            // The following line could have used Int32.Parse
            //  or the shortcut int.Parse instead of Convert.ToInt32.
            //
            int salary = Convert.ToInt32(Console.ReadLine());
            // The C format specifier indicates that we want
            //  to display the salary in currency format.
            //
            Console.WriteLine("Your salary is {0:C}", salary);

            // DateTime.Now is a static property of the DateTime
            //  class that returns the current date and time.
            //
            DateTime now = DateTime.Now;
            
            Console.WriteLine("Today is {0}", now);
            // The D format specifier indicates that we want
            //  to display the date in 'long date format'.
            //
            Console.WriteLine("To be rigorous, it's {0:D}", now);

            // To use the CultureInfo class, we add a 'using' statement
            //  for the System.Globalization namespace.  The CultureInfo
            //  class allows us to use pre-defined formatting rules for
            //  various cultures.  "fr-BE" is the French-Belgium culture.
            //
            CultureInfo belgianCulture = CultureInfo.GetCultureInfo("fr-BE");
            string dateInBelgium = now.ToString("D", belgianCulture);
            Console.WriteLine("In the Vallon part of Belgium, though, it's {0}", dateInBelgium);
        }
    }
}
