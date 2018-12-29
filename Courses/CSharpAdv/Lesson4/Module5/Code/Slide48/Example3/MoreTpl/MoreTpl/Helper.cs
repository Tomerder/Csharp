using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Slide13
{
    class Helper
    {
        internal static void A()
        {
            Random r = new Random();
            Thread.Sleep(r.Next(1000));
            Console.WriteLine("A");
        }

        internal static void B()
        {
            Random r = new Random();
            Thread.Sleep(r.Next(1000));
            Console.WriteLine("B");
        }

        internal static int C()
        {
            Thread.Sleep(1000);
            return 3;
        }

    }
}
