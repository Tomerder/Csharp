using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Slide27
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(i);
            }

            //var list2 = from n in list
            //            select n;

            //foreach (var n in list2)
            //{
            //    Console.WriteLine(n);
            //}

            //var list3 = from n in list
            //                .AsParallel()
            //            select n;

            //foreach (var n in list3)
            //{
            //    Console.WriteLine(n);
            //}

            //var list2 = from n in list
            //            select new { N = n, MyThread = Thread.CurrentThread.ManagedThreadId };

            //foreach (var n in list2)
            //{
            //    Console.WriteLine(n.N + " " + n.MyThread);
            //}

            //var list2 = from n in list
            //             .AsParallel()
            //            select new { N = n, MyThread = Thread.CurrentThread.ManagedThreadId };

            //foreach (var n in list2)
            //{
            //    Console.WriteLine(n.N + " " + n.MyThread);
            //}

            var list2 = from n in list
                        .AsParallel()
                        select new { N = n, MyThread = Thread.CurrentThread.ManagedThreadId };

            var list3 = list2.OrderBy(l => l.MyThread);

            foreach (var n in list3)
            {
                Console.WriteLine(n.N + " " + n.MyThread);
            }
        }
    }
}
