using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace GC_OrderProcessing
{
    public class OrderProcessor
    {
        private List<Order> _orderCache = new List<Order>();
        private static readonly int _orderCacheMaxSize = 10000;
        private static readonly Random _random = new Random();
        private int _totalOrdersProcessed;
        private Stopwatch _stopper = Stopwatch.StartNew();

        public void Process(Order order)
        {
            if (++_totalOrdersProcessed % 1000000 == 0)
            {
                Console.WriteLine("Total orders processed: " + _totalOrdersProcessed);
                Console.WriteLine("Ticks per order: " + ((double)_stopper.ElapsedTicks) / _totalOrdersProcessed);
            }

            if (_orderCache.Count >= _orderCacheMaxSize)
            {
                int index = _random.Next(0, _orderCache.Count);
                _orderCache[index] = order;
            }
            else
            {
                _orderCache.Add(order);
            }
        }
    }
}
