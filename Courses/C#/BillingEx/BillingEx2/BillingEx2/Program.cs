using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx2
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer cust1 = new Customer("John");
            Customer cust2 = new Customer("Smith", 100.0);

            BillingSystem bs1 = new BillingSystem(2);
            bs1.addCustomer(cust1);
            bs1.addCustomer(cust2);


            Console.WriteLine(bs1);

            Console.ReadLine();
        }
    }
}
