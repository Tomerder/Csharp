
namespace LockExample
{
    /// <summary>
    /// A bank account object that is protected with manual synchronization
    /// using the C# 'lock' statement.
    /// </summary>
    class BankAccount
    {
        public decimal Balance { get; private set; }

        //A private object to use for synchronization.  Note that using the
        //enclosing object ('this') or any other public object for synchronization
        //is dangerous because it is accessible (and can be locked) by code
        //external to the class.  This can introduce potential deadlocks between
        //the class code and its clients.
        private readonly object _syncRoot = new object();

        public void Deposit(decimal amount)
        {
            //Modification of the balance is performed within a lock:
            lock (_syncRoot)
            {
                Balance += amount;
            }
        }

        public void Withdraw(decimal amount)
        {
            //Modification of the balance is performed within a lock:
            lock (_syncRoot)
            {
                Balance -= amount;
            }
        }
    }
}
