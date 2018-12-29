using System;
using System.Collections.Generic;
using System.Text;

namespace GC_OrderProcessing
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            OrderReceiver receiver = new OrderReceiver();
            receiver.Run();
        }
    }
}
