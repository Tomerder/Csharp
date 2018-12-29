using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx5
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer cust1 = new RegularCustomer("John");
            Customer cust2 = new VIPCustomer("Smith", 100.0);
            Customer cust3 = new VIPCustomer("Smith", 100.0);
            try
            {
                BillingSystem bs1 = new BillingSystem(2);
                bs1.addCustomer(cust1);
                bs1.addCustomer(cust2);
                bs1.addCustomer(cust3);

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
                Console.WriteLine(bs1[1, "John"]);

                Console.WriteLine("Customer 3 searched");
                Console.WriteLine(bs1[3]);

            }
            catch (TooManyCustomersExcpetion e)
            {
                Console.WriteLine("Exception caught, type: {0}", e.GetType());
                Console.WriteLine("Too many customers already in the system, current number is: {0}", e.MaxCutomers);
                Console.WriteLine(e);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            finally
            {

                Console.ReadLine();
            }
        }
    }
}
