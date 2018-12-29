using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx3
{
    class BillingSystem
    {
        // internal data
        private Customer[] _customers;
        private int _numCustomers;
        const int defaultSize = 100;

        //Constructors
        public BillingSystem(int size)
        {
            _customers = new Customer[size];
            _numCustomers = 0;
        }
        public BillingSystem() : this(defaultSize) { }

        public bool addCustomer(Customer cust)
        {
            if (_numCustomers >= _customers.Length)
                return false; // can not add anymore!
            _customers[_numCustomers++] = cust;
            return true;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Billing System Data:");
            //foreach (Customer cust in _customers)
            // we will not iterate on all the array, just until _numCustomers.
            // the foreach statement does not count iterations. Therefor
            // it will be simpler to use a for loop
            for (int i = 0; i < _numCustomers; i++)
            {

                sb.AppendLine(_customers[i].ToString());
            }
            return sb.ToString();
        }

        public void updateBalance(double amount)
        {
            for (int i = 0; i < _numCustomers; i++)
            {

                _customers[i].addToBalance(amount);
            }
        }


    }
}
