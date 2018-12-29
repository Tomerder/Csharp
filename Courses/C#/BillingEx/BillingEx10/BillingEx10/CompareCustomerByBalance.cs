using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BillingEx10
{
    class CompareCustomerByBalance : IComparer<Customer>
    {
        //public int Compare(object x, object y)
        //{
        //    Customer cust1 = x as Customer;
        //    Customer cust2 = y as Customer;
        //    if (cust1 == null || cust2 == null)
        //        throw new InvalidCastException();
        //    return cust1.Balance.CompareTo(cust2.Balance);
        //}

        public int Compare(Customer cust1, Customer cust2)
        {
            if (cust1 == null || cust2 == null)
                throw new InvalidCastException();
            return cust1.Balance.CompareTo(cust2.Balance);
        }
    }
}
