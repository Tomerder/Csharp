using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    class Program
    {
        static Func<int> f;
        static void Main(string[] args)
        {
            f = F;
            f.BeginInvoke(MyCallback, null);
            Console.ReadLine();
        }

        static int F()
        {
            return 5;
        }

        static void MyCallback(IAsyncResult ar)
        {
            AsyncResult a = (AsyncResult)ar;
            Func<int> aaa = (Func<int>)a.AsyncDelegate;
            int result = aaa.EndInvoke(ar);
            Console.WriteLine(result);
        }
    }
}
