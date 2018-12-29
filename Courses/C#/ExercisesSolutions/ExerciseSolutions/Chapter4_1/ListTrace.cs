using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter4Exercise1
{
    public partial class LinkedList<T>
    {
        static partial void Log(string message, T item, int count)
        {
            Console.WriteLine("Action: {0}, Item: {1}, Number Of Elements: {2}", message, item, count);
        }
    }
}
