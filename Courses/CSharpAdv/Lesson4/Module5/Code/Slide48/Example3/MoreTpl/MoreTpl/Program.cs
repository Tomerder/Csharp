using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Slide13
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demo1();
            // Demo2();
            // Demo3();
            // Demo4();
            // Demo5();
            // Demo6();
             Demo7();
        }

        private static void Demo1()
        {
            Task.Factory.StartNew(Helper.A);
            Task.Factory.StartNew(Helper.B);
            Console.ReadLine();
        }

        private static void Demo2()
        {
            Task task1 = new Task(new Action(Helper.A));
            Task task2 = new Task(Helper.B);

            Task task3 = new Task(
                delegate()
                {
                    Random r = new Random();
                    Thread.Sleep(r.Next(1000));
                    Console.WriteLine("annon");
                });

            Task task4 = new Task(
                () =>
                {
                    Random r = new Random();
                    Thread.Sleep(r.Next(1000));
                    Console.WriteLine("lambda");
                });

            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();

            Console.ReadLine();
        }

        private static void Demo3()
        {
            Task<int> task = new Task<int>(Helper.C);
            task.Start();
            Console.WriteLine(task.Result);
        }

        private static void Demo4()
        {
            Task<int> task = new Task<int>(() => { Thread.Sleep(1000); return 4; });
            task.Start();
            Console.WriteLine(task.Result);
        }

        private static void Demo5()
        {
            Task<int> task = Task<int>.Factory.StartNew(() => { Thread.Sleep(1000); return 5; });
            Console.WriteLine(task.Result);
        }

        private static void Demo6()
        {
            Task t1 = Task.Factory.StartNew(() => { Thread.Sleep(2000); Console.WriteLine("t1"); });
            Task t2 = Task.Factory.StartNew(() => { Thread.Sleep(1000); Console.WriteLine("t2"); });
            Console.WriteLine("Before");
            Task.WaitAll(t1, t2);
            Console.WriteLine("After");
            Console.ReadLine();
        }

        private static void Demo7()
        {
            Task t1 = Task.Factory.StartNew(() => { Thread.Sleep(2000); Console.WriteLine("t1"); });
            Task t2 = Task.Factory.StartNew(() => { Thread.Sleep(1000); Console.WriteLine("t2"); });
            Console.WriteLine("Before");
            Task.WaitAny(t1, t2);
            Console.WriteLine("After");
            Console.ReadLine();
        }
    }
}
