using System;
using System.Threading;

namespace MonitorExample
{
    class Program
    {
        const int INCREMENTS = 10000;
        const int PARALLELISM = 10;

        static void Main(string[] args)
        {
            BankAccountWithMonitor myAccount = new BankAccountWithMonitor();
            myAccount.Deposit(10000m);

            for (int i = 0; i < PARALLELISM; ++i)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    for (int k = 0; k < INCREMENTS; ++k)
                    {
                        myAccount.Deposit(10);
                        myAccount.Withdraw(10);
                    }
                });
            }

            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine("Account balance: {0} (expected 10000)", myAccount.Balance);
        }
    }
}
