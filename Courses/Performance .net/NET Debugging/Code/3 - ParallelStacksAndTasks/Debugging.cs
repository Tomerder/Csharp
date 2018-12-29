using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.MSDNEvent.ParallelismVS2010
{
    class Debugging
    {
        //Parallel Tasks (Debug --> Windows --> Parallel Tasks)
        static void Deadlock()
        {
            object _1 = new object();
            object _2 = new object();
            Task a = Task.Factory.StartNew(() =>
                { lock (_1) { Thread.Sleep(100); lock (_2) { } } });
            Task b = Task.Factory.StartNew(() =>
                { lock (_2) { Thread.Sleep(100); lock (_1) { } } });
            Task c = Task.Factory.StartNew(() => Thread.Sleep(10000));
            Parallel.ForEach(Enumerable.Range(0, 100000),
                _ => { Thread.Yield(); });

            Task.WaitAll(a, b);
        }

        //These functions are the common path for all the tasks.
        static void A()
        {
            Thread.Sleep(100);
            B();
        }
        static void B()
        {
            Thread.Sleep(100);
            C();
        }
        static void C()
        {
            Thread.Sleep(100);
            D();
        }

        static Random _rand = new Random();
        static void D()
        {
            int value;
            lock (_rand)
            {
                value = _rand.Next(0, 4);
            }
            switch (value)
            {
                case 0:
                    goto case 2;
                case 1:
                    D1();
                    break;
                case 2:
                    D2();
                    break;
                case 3:
                    D3();
                    break;
                default:
                    break;
            }
        }
        static void D1()
        {
            Thread.Sleep(2000);
        }
        static void D2()
        {
            Thread.Sleep(4000);
        }
        static void D3()
        {
            Thread.Sleep(3000);
        }

        //Parallel Stacks (Debug --> Windows --> Parallel Stacks)
        static void TasksWithComplexCallStacks()
        {
            for (int i = 0; i < 5; ++i)
            {
                Task.Factory.StartNew(A).ContinueWith(
                    _ => Task.Factory.StartNew(A));
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Deadlock();
            //TasksWithComplexCallStacks();
        }
    }
}
