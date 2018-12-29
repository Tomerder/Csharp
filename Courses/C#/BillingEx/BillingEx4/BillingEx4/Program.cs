using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx4
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer cust1 = new RegularCustomer("John");
            Customer cust2 = new VIPCustomer("Smith", 100.0);

            BillingSystem bs1 = new BillingSystem(2);
            bs1.addCustomer(cust1);
            bs1.addCustomer(cust2);

            Console.WriteLine("Before adding 100 to balance");
            Console.WriteLine("============================");
            Console.WriteLine(bs1);

            bs1.updateBalance(100);

            Console.WriteLine("After adding 100 to balance");
            Console.WriteLine("============================");
            Console.WriteLine(bs1);

            Console.WriteLine("Customer John searched");
            Console.WriteLine(bs1["John"]);

            Console.WriteLine("Customer 1, John searched");
            Console.WriteLine(bs1[1,"John"]);

            Console.WriteLine("Customer 1 searched");
            Console.WriteLine(bs1[1]);


            Console.ReadLine();
        }
    }
}
