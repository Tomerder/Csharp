using System;
using System.Collections.Generic;
using System.Text;

namespace GC_OrderProcessing
{
    public class OrderFilterer
    {
        private OrderProcessor _processor = new OrderProcessor();

        public void Filter(Order order)
        {
            if (order.OrderId % 4 == 0)
                return;

            _processor.Process(order);
        }
    }
}
