using System;

namespace Generics
{
    static class VectorSorter<T> where T : IComparable
    {
        public static void Sort(Vector<T> vec)
        {
            // Sort implementation omitted for brevity.
            //  Sample of what we can do now:
            //
            int result = vec[0].CompareTo(vec[1]);

            // We can invoke the CompareTo method because
            //  this class has a generic constraint (T : IComparable).
        }
    }
}
