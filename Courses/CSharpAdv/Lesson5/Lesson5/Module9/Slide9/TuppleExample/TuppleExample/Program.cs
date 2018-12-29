using System;

namespace TuppleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var record = new Tuple<int, string, decimal>(1, "Jack", 14000m);
            Console.WriteLine(record.Item2);
        }
    }
}
