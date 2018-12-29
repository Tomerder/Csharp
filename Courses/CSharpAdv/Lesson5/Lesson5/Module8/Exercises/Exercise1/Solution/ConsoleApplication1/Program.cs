using System;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Action del = new Action(F);
            del += G;
            Invoke(del);
            Console.ReadLine();
        }

        static void F()
        {
            Console.WriteLine("F");
        }

        static void G()
        {
            Console.WriteLine("G");        
        }

        static void Invoke(Action del)
        { 
            foreach (Delegate d in del.GetInvocationList())
            {
                ThreadPool.QueueUserWorkItem(state => { d.DynamicInvoke(); });
            }
        }
    }
}
