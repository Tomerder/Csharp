using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GC_OrderProcessing
{
    public class OrderReceiver
    {
        private OrderFilterer _filterer = new OrderFilterer();

        public void Run()
        {
            while (true)
            {
                Order order = new Order(167);

                if (order.OrderId % 1000 == 0)
                    Thread.Sleep(0);

                _filterer.Filter(order);
            }
        }
    }
}
