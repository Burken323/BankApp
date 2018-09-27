using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Account
    {
        public int accountNumber { get; set; }
        public int customerId { get; set; }
        public decimal balance { get; set; }
        public int interest { get; set; }
        public int credit { get; set; }

        public Account(int accNum, int custId, decimal bal)
        {
            accountNumber = accNum;
            customerId = custId;
            balance = bal;

        }
    }
}
