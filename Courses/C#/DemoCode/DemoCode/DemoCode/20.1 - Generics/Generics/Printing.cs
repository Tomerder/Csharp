using System;
using System.Collections.Generic;

namespace Generics
{
    interface IPrintable<T>
    {
        void Print(T value);
    }

    class PrintableClass : IPrintable<string>, IPrintable<int> , IPrintable <PrintableClass>
    {
        public void Print(string p)
        {
            Console.WriteLine("Printable!");
        }
        public void Print(int i)
        {
        }

        public void Print(PrintableClass value)
        {
            throw new NotImplementedException();
        }
    }

    static class Printing
    {
        public static void PrintAll<T>(IEnumerable<T> collection)
            where T : IPrintable<T>
        {
            foreach (T element in collection)
            {
                element.Print(element);
            }
        }
    }
}
