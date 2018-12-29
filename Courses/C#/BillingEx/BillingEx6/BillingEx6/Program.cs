using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx6
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer cust1 = new RegularCustomer("John");
            Customer cust2 = new VIPCustomer("Smith", -100.0);
            Customer cust3 = new VIPCustomer("aaa", 100.0);
            try
            {
                BillingSystem bs1 = new BillingSystem(3);
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

                //Console.WriteLine("Customer 3 searched");
                //Console.WriteLine(bs1[3]);

                Console.WriteLine("Before sort");
                Console.WriteLine("===========");
                Console.WriteLine(bs1);

                //bs1.Sort();
                bs1.Sort(new CompareCustomerByBalance());

                Console.WriteLine("After sort");
                Console.WriteLine("===========");
                Console.WriteLine(bs1);

                Console.WriteLine("Iterator issues");
                foreach (Customer cust in bs1)
                    Console.WriteLine(cust);

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
