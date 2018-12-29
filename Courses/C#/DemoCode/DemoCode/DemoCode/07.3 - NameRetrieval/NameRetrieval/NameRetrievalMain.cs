using System;

namespace NameRetrieval
{
    class NameRetrievalMain
    {
        static void Main(string[] args)
        {
            NameSorter sorter = new NameSorter(@"RawNames.txt");
            sorter.Read();
            sorter.Sort();
            sorter.WriteTo(@"SortedNames.txt");
        }
    }
}
