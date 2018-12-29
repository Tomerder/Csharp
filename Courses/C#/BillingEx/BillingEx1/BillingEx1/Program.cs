using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx
{
    class Program
    {
        static void Main(string[] args)
        {

            Customer cust1 = new Customer("John");
            Customer cust2 = new Customer("Smith", 100.0);

            Console.WriteLine(cust1);
            Console.WriteLine(cust2);

            Console.ReadLine();
        }
    }
}
