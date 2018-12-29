﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillingEx8
{
    public class IrregularBalanceEventArgs :EventArgs
    {
        public IrregularBalanceEventArgs(Customer cust, double balance)
        {
            Balance = balance;
            Suspect = cust;
        }
        // keeps the balance of the customer whose balance is too high
        public double Balance { get;  set; }
        public Customer Suspect { get; set; }

    }
}