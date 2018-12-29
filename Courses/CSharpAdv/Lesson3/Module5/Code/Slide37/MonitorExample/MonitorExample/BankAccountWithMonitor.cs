using System.Threading;

namespace MonitorExample
{
    /// <summary>
    /// An equivalent example of a bank account object that manually calls
    /// Monitor.Enter and Monitor.Exit to achieve synchronization.  Note that
    /// all work is enclosed in try...finally clauses so that the monitor is
    /// always released before the method returns.
    /// </summary>
    class BankAccountWithMonitor
    {
        public decimal Balance { get; private set; }

        private readonly object _syncRoot = new object();

        public void Deposit(decimal amount)
        {
            //Absolutely equivalent to using the 'lock' statement
            Monitor.Enter(_syncRoot);
            try
            {
                Balance += amount;
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }

        public void Withdraw(decimal amount)
        {
            //Absolutely equivalent to using the 'lock' statement
            Monitor.Enter(_syncRoot);
            try
            {
                Balance -= amount;
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }
    }
}
