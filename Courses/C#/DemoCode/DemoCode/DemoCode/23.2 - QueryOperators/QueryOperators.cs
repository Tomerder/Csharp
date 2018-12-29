using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch3_QueryOperators
{
    class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    static class EnumerableExtensions
    {
        class WhereEnumerable<T> : IEnumerable<T>
        {
            IEnumerable<T> source;
            Predicate<T> filter;

            public WhereEnumerable(IEnumerable<T> source, Predicate<T> filter)
            {
                this.source = source;
                this.filter = filter;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new WhereEnumerator(this);
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            class WhereEnumerator : IEnumerator<T>
            {
                WhereEnumerable<T> enumerable;
                IEnumerator<T> sourceEnumerator;
                T current;
                bool initialized;

                public WhereEnumerator(WhereEnumerable<T> enumerable)
                {
                    this.enumerable = enumerable;
                    this.sourceEnumerator = enumerable.source.GetEnumerator();
                }

                public T Current
                {
                    get
                    {
                        if (!initialized)
                            throw new InvalidOperationException();
                        return current;
                    }
                }

                public void Dispose()
                {
                }

                object System.Collections.IEnumerator.Current
                {
                    get { return this.Current; }
                }

                public bool MoveNext()
                {
                    initialized = true;

                    T candidate = default(T);
                    bool found = false;
                    while (sourceEnumerator.MoveNext())
                    {
                        candidate = sourceEnumerator.Current;
                        if (enumerable.filter(candidate))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        current = candidate;
                    return found;
                }

                public void Reset()
                {
                    throw new NotSupportedException();
                }
            }
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Predicate<T> filter)
        {
            foreach (T elem in source)
                if (filter(elem))
                    yield return elem;

            //return new WhereEnumerable<T>(source, filter);
        }

        public static IEnumerable<T> Order<T>(this IEnumerable<T> source)
        {
            SortedDictionary<T, bool> elements = new SortedDictionary<T, bool>();
            foreach (T elem in source)
                elements.Add(elem, default(bool));
            foreach (T key in elements.Keys)
                yield return key;
        }

        public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, Func<T, S> projection)
        {
            foreach (T elem in source)
                yield return projection(elem);
        }

        public static long Count<T>(this IEnumerable<T> source)
        {
            long count = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
                ++count;
            return count;
        }

        public static T First<T>(this IEnumerable<T> source)
        {
            var enumerator = source.GetEnumerator();
            if (enumerator.MoveNext())
                return enumerator.Current;
            throw new InvalidOperationException("Empty source");
        }
    }

    class QueryOperators
    {
        static IEnumerable<int> Range(int min, int max)
        {
            for (; min < max; ++min)
                yield return min;
        }

        static void Main(string[] args)
        {
            foreach (int num in Range(0, 100))
                Console.WriteLine(num);

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int even in nums.Where(i => i % 2 == 0))
            {
                Console.WriteLine(even);
            }

            Employee[] employees = {
                                       new Employee { Id=14, Name="John" },
                                       new Employee { Id=12, Name="Mary" },
                                       new Employee { Id=31, Name="Bob" },
                                       new Employee { Id=6, Name="Alice" }
                                   };
            var sortedEmpIds = employees.Select(e => e.Id).Order();

            //These two variables are deferred queries
            var numbers = Range(0, 100);
            var query = numbers.Where(i => i % 2 == 0);

            //query.Count() evaluates the query
            Console.WriteLine("{0} even numbers found, the first one is {1}",
                query.Count(), query.First());

            //This explicitly evaluates the query
            foreach (int i in query)
                Console.WriteLine(i);

            //Examples of LINQ operators:
            var q =
                Enumerable.Range(0, 100)
                    .Select(i => new Employee { Id = i, Name = i.ToString() })
                    .Where(e => e.Name.StartsWith("1"))
                    .OrderByDescending(e => e.Id)
                    .GroupBy(e => e.Name.Substring(e.Name.Length - 1))
                    .Select(grp => grp);
            foreach (var group in q)
            {
                Console.WriteLine("Key: " + group.Key);
                Console.WriteLine("Elements: " + group.Aggregate("", (a, e) => a += e.Name + " "));
            }

            //Examples of language query operators:
            var q2 = from i in Enumerable.Range(0, 100)
                    select new { Id = i, Name = i.ToString() };
            var q3 = from e in q2
                     where e.Name.StartsWith("1")
                     orderby e.Id descending
                     group e by e.Name.Substring(e.Name.Length - 1) into g
                     select g;
            foreach (var group in q3)
            {
                Console.WriteLine("Key: " + group.Key);
                Console.WriteLine("Elements: " + group.Aggregate("", (a, e) => a += e.Name + " "));
            }

            //Mixing query operators:
            int cnt = (from i in Enumerable.Range(0, 100)
                       where i % 2 == 0
                       select i).Count(i => i > 25);
        }
    }
}
