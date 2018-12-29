using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    delegate void MyDel(int x);

    class Program
    {
        static void Main(string[] args)
        {
            MyDel myDel = new MyDel(F);
            AsyncCallback callback = new AsyncCallback(CallbackMethod);
            myDel.BeginInvoke(1000, callback, null);
            Console.WriteLine("Main Before");
            Thread.Sleep(3000);
            Console.WriteLine("Main After");
        }

        static private void F(int id)
        {
            Thread.Sleep(id);
            Console.WriteLine("F");
            Thread.Sleep(id);
        }

        static private void CallbackMethod(IAsyncResult iar)
        {
            Console.WriteLine("CallbackMethod");
        }
    }
}
