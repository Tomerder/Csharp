using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BillingEx9
{

    public delegate void IrregularBalanceEventHandler(object sender, IrregularBalanceEventArgs args);

    class BillingSystem :IEnumerable
    {
        // internal data
        //private Customer[] _customers;
        private List<Customer> _customers;

        //private int _numCustomers;
        const int defaultSize = 100;

        public event IrregularBalanceEventHandler irregBalanceListner;

        const double TARIFF = 0.1; // general cost of a time unit
        const double MAX_BALANCE_ALLOWED = 10000;

        //Constructors
        public BillingSystem(int size)
        {
           // _customers = new Customer[size];
            _customers = new List<Customer>(size);
            //_numCustomers = 0;
        }
        public BillingSystem() : this(defaultSize) { }

        public bool addCustomer(Customer cust)
        {
            //if (_numCustomers >= _customers.c)
            //    //return false; // can not add anymore!
            //   // throw new IndexOutOfRangeException(); // this does not really tell the user what happens,
            //                                            // although its technically right.
            //    throw new TooManyCustomersExcpetion(_customers.Length);
            //_customers[_numCustomers++] = cust;
           
            _customers.Add(cust);
                
            return true;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Billing System Data:");
            foreach (Customer cust in _customers)
            {

                sb.AppendLine(cust.ToString());
            }
            return sb.ToString();
        }

        // whenever we update customer balance, for any reason, we
        // should use this method, to make sure we check and fire the event if needed.
        private void addToCustomerBalance(Customer cust, double cost)
        {

            cust.addToBalance(cost);
            if (cust.Balance >= MAX_BALANCE_ALLOWED) // PROBLEM! Customer ows too much money!
            {
                IrregularBalanceEventArgs args = new IrregularBalanceEventArgs(cust, cost);

                if (irregBalanceListner != null)
                    irregBalanceListner(this, args);

            }
        }

        public void ChargingCalls()
        {

            // let us assume that what really happens here is that customers make calls
            // and the billing system calculates how much to add for their bills for the calls
            Random rnd = new Random();
            foreach (Customer cust in _customers)
            {

                // _customers[i].addToBalance(amount);
                addToCustomerBalance(cust, 1000 * rnd.NextDouble() );
            }
        }

        public void updateBalance(double amount)
        {
            foreach (Customer cust in _customers)
            {

               // _customers[i].addToBalance(amount);
                addToCustomerBalance(cust, amount); // use the inner method, so that checks of limit will be made
            }
        }

        // retrieves customer with name name
        public Customer this[string name]
        {
            get
            {

                foreach (Customer cust in _customers)
                {

                    if (cust.Name.Equals(name))
                        return cust;
                }
                return null;
            }

        }
        // retrieves customer with id. If name is not name, null is returned.
        public Customer this[int id, string name]
        {
            get
            {

                foreach (Customer cust in _customers)
                {

                    if (cust.Id.Equals(id))
                        if (cust.Name.Equals(name))
                            return cust;
                        else
                            //    return null; // this should not happen: ids are unique!
                            throw new ArgumentException("ID does not match the required name");
                }
                return null;
            }

        }
        // retrieves the index'th customer in the billing internal list.
        public Customer this[int index]
        {
            get
            {
               
                if (index >= 0 && index < _customers.Count)
                        return _customers.ElementAt(index);
               
                throw new IndexOutOfRangeException();
            }
            private set
            {
                if (index >= 0 && index < _customers.Count)
                {
                    _customers[index] = value;
                }
                else
                    throw new IndexOutOfRangeException();
                // else we really should report the error . How?
            }

        }

        public void Sort()
        {
            //Array.Sort(_customers);
            _customers.Sort();
        }

        public void Sort(IComparer criteria)
        {
            //Array.Sort(_customers, criteria);
            Comparison<Customer> crt = criteria.Compare;
            _customers.Sort(crt);
        }

        // this is an advance issue, ignore if currently not covered in class.
        public IEnumerator GetEnumerator()
        {
            foreach (Customer cut in _customers)
                yield return cut;
        }

        

       


    }
}
