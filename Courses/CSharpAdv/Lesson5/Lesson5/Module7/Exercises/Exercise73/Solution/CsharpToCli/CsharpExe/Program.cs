using System;

namespace CsharpExe
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorNamespace.Calculator calculator = new CalculatorNamespace.Calculator();
            calculator.Plus(1, 2);
            calculator.PlusResult += Subscriber.Print;
            calculator.Plus(2, 3);
            calculator.PlusResult += Subscriber.Print;
            calculator.Plus(1, 2);
        }
    }

    class Subscriber
    {
        public static void Print(int res)
        {
            Console.WriteLine(res);
        }
    }
}

