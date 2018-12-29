using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public static class EnumerableExtensions
    {
        // helper reflection methods. To speed things up a little
        private static MethodInfo EmptyMethod = typeof(Enumerable).GetMethods().Where(m => m.Name == "Empty").FirstOrDefault();
        private static MethodInfo CastMethod = typeof(Enumerable).GetMethod("Cast", new[] { typeof(IEnumerable) });
        private static MethodInfo ToListMethod = typeof(Enumerable).GetMethods().Where(m => m.Name == "ToList").FirstOrDefault();

        public static IEnumerable<T> AsIEnumerable<T>(this T obj)
        {
            yield return obj;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, params T[] items)
        {
            return source.Concat(items.AsEnumerable());
        }

        public static IList CreateGenericList(this Type itemType)
        {
            var emptyEnum = EmptyMethod
                                .MakeGenericMethod(new[] { itemType })
                                .Invoke(null, new object[] { });

            var list = ToListMethod
                            .MakeGenericMethod(new[] { itemType })
                            .Invoke(null, new object[] { emptyEnum });

            return list as IList;
        }

        public static List<T> CreateGenericList<T>(IEnumerable<T> values)
        {
            var list = ToListMethod
                        .MakeGenericMethod(new[] { typeof(T) })
                        .Invoke(null, new object[] { values });

            return list as List<T>;
        }

        public static IList CreateGenericList(this Type itemType, IEnumerable values)
        {
            // turn values into IEnumerable<itemType>
            var ienum = CastMethod
                        .MakeGenericMethod(itemType)
                        .Invoke(null, new object[] { values });

            // now create a list
            var list = ToListMethod
                        .MakeGenericMethod(itemType)
                        .Invoke(null, new object[] { ienum });

            return list as IList;

        }

        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> values1, IEnumerable<T> values2)
        {
            var i1 = values1.GetEnumerator();
            var i2 = values2.GetEnumerator();

            while ((i1.MoveNext()) || (i2.MoveNext()))
            {
                yield return new Tuple<T, T>(i1.Current, i2.Current);
            }
        }

        public static IEnumerable<T> FirstEquals<T>(this IEnumerable<T> values1, IEnumerable<T> values2)
        {
            var i1 = values1.GetEnumerator();
            var i2 = values2.GetEnumerator();

            while ((i1.MoveNext() && i2.MoveNext() && (i1.Current.Equals(i2.Current))))
            {
                yield return i1.Current;
            }
        }

        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> recursiveSelector)
        {
            foreach (var i in source)
            {
                yield return i;

                var directChildren = recursiveSelector(i);
                var allChildren = SelectRecursive(directChildren, recursiveSelector);

                foreach (var c in allChildren)
                {
                    yield return c;
                }
            }
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> comparer)
        {
            return source.Distinct(comparer.ToEqualityComparer());
        }

        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int index = 0;
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

    }
}
