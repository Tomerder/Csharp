using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiThreadStackActivator activator = new MultiThreadStackActivator();
            activator.ActivateB();                  
        }

        //---------------------------------------------------------------------

    }
}
