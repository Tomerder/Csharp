using System;

namespace Generics
{
    class GenericsDemo
    {
        #region Generic Methods
        
        static void Swap<T>(ref T a, ref T b)
        {
            // No constraints are necessary here because operator= is defined
            //  for every type.
            T temp = a;
            a = b;
            b = temp;
        }

        static void PrintDefaultValue<T>()
        {
            Console.WriteLine("Default value for type: {0} is: {1}",
                              typeof(T).Name, default(T));
        }

        #endregion
        
        static void Main(string[] args)
        {
            #region Using Vector<T>

            // To create an instance of the Vector<T> type,
            //  we must provide a type parameter.            
            Vector<int> first = new Vector<int>(5);
            first.Fill(3);
            Console.WriteLine(first.ToString());

            Vector<int> second = new Vector<int>(5);
            second.Fill(2);
            second[3] = 14;
            Console.WriteLine(second.ToString());

            #endregion

            #region Using VectorSorter<T>

            VectorSorter<int>.Sort(second);

            #endregion

            #region Using Printables

            PrintableClass[] printables = new PrintableClass[2];
            printables[0] = new PrintableClass();
            printables[1] = new PrintableClass();
            Printing.PrintAll(printables);

            #endregion

            #region Using default(T)

		    PrintDefaultValue<int>();
            PrintDefaultValue<bool>();
            PrintDefaultValue<string>();

	        #endregion
        }
    }
}
