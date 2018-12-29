using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication2
{
    delegate void MyDel(int x);

    class Program
    {
        static void Main(string[] args)
        {
            MyDel myDel = new MyDel(F);
            AsyncCallback callback = new AsyncCallback(CallbackMethod);
            StringBuilder state = new StringBuilder("Main");
            IAsyncResult iar = myDel.BeginInvoke(1, callback, state);
            Thread.Sleep(1000);
            Console.WriteLine(state);
        }

        static private void F(int id)
        {
            Console.WriteLine(id);
        }

        static private void CallbackMethod(IAsyncResult iar)
        {
            StringBuilder state = (StringBuilder)iar.AsyncState;
            Console.WriteLine("State recevied by CallbackMethod: " + state.ToString());
            state.Append(" Callback finished successfully");
        }
    }
}
