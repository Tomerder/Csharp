using System;

namespace Chapter20Exercise1
{
    public static class ListUtil
    {
        public static void Print<T>(this LinkedList<T> list) where T:IComparable<T>
        {
            for (int i = 0; i < list.Count; ++i)
                Console.Write("{0}, ", list.GetAt(i));
            Console.WriteLine();
        }
    }
}
