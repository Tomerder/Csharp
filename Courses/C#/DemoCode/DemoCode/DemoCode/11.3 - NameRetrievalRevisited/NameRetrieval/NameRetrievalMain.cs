using System;

namespace NameRetrieval
{
    class NameRetrievalMain
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting processing...");
                NameSorter sorter = new NameSorter(@"RawNames.txt");
                sorter.Read();
                sorter.Sort();
                sorter.WriteTo(@"SortedNames.txt");
                Console.WriteLine("Processing completed successfully.");
            }
            catch (BadFileFormatException e)
            {
                Console.WriteLine("Exception caught in main:\n" + e);
            }
        }
    }
}
