
using System;

namespace Chapter20Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            list.Print();

            System.Collections.Generic.IList<int> iList = list;

            iList.RemoveAt(5);
            iList.RemoveAt(2);

            list.Print();

            iList.Insert(3, 100);

            iList[3]++;

            foreach (int i in iList)
                Console.Write("{0}|", i);

            list.Clear();
            list.Print();
            list.Add(5);
            list.Print();
        }
    }
}
