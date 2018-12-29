using System;
using System.Collections.Generic;
using System.Text;

namespace GC_OrderProcessing
{
    public class Order
    {
        private static int _orderCounter;

        public Order(int customerId)
        {
            _customerId = customerId;
            _orderId = _orderCounter++;
            _orderTime = DateTime.Now;
        }

        private DateTime _orderTime;

        public DateTime OrderTime
        {
            get { return _orderTime; }
            set { _orderTime = value; }
        }

        private int _customerId;

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private int _orderId;

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
    }
}
