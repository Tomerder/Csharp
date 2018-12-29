using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t1 = new Task(F);
            t1.Start();

            Task t2 = Task.Factory.StartNew(F);

            Task t3 = Task.Run(() => Console.Write("aaa"));
        }

        static void F()
        { }
    }
}
