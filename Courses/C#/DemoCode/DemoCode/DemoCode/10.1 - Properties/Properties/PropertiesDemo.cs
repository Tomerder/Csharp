using System;

namespace Properties
{
    class PropertiesDemo
    {
        static void Main(string[] args)
        {
            // Using the SimpleDate class is not very convenient -
            //  the user would like to access the fields using the
            //  simple field access operator (instance.field), but
            //  instead is forced to use methods.
            //
            SimpleDate simpleDate = new SimpleDate(1, 1, 2000);
            DisplaySimpleDate(simpleDate);
            simpleDate.SetDay(20);
            simpleDate.SetMonth(12);
            DisplaySimpleDate(simpleDate);

            // Using the DateWithProperties class, on the other hand,
            //  is highly intuitive.  Even though we can use the
            //  convenient field access operation (instance.field),
            //  a method is invoked for us by the C# compiler.
            //
            DateWithProperties date = new DateWithProperties(1, 1, 2000);
            DisplayDateWithProperties(date);
            date.Day = 20;
            date.Month = 12;
            DisplayDateWithProperties(date);
        }

        private static void DisplaySimpleDate(SimpleDate simpleDate)
        {
            Console.WriteLine("The date is: {0}/{1}/{2}",
                              simpleDate.GetDay(),
                              simpleDate.GetMonth(),
                              simpleDate.GetYear());
        }

        private static void DisplayDateWithProperties(DateWithProperties date)
        {
            Console.WriteLine("The date is: {0}/{1}/{2}",
                              date.Day,
                              date.Month,
                              date.Year);
        }
    }
}
