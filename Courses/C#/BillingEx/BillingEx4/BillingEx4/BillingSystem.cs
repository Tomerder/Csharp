using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx4
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

        // retrieves customer with name name
        public Customer this[string name]
        {
            get
            {
               
                for (int i = 0; i < _numCustomers; i++)
                {

                    if (_customers[i].Name.Equals(name))
                        return _customers[i];
                }
                return null;
            }

        }
        // retrieves customer with id. If name is not name, null is returned.
        public Customer this[int id, string name]
        {
            get
            {

                for (int i = 0; i < _numCustomers; i++)
                {

                    if (_customers[i].Id.Equals(id))
                        if (_customers[i].Name.Equals(name))
                            return _customers[i];
                        else
                            return null; // this should not happen: ids are unique!
                }
                return null;
            }

        }
        // retrieves the index'th customer in the billing internal list.
        // currently we return null in case of a wrong index.
        // see Exception handling chapter for better solution
        public Customer this[int index]
        {
            get
            {

                if (index >= 0 && index < _customers.Length)
                    return _customers[index];
                return null;
            }
            private set
            {
                if (index >= 0 && index < _customers.Length)
                {
                    _customers[index] = value;
                }
                // else we really should report the error . How?
            }

        }

        


    }
}
