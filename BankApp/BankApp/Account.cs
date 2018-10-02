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
        public List<Transaction> transactions;

        public Account(int accNum, int custId, decimal bal)
        {
            accountNumber = accNum;
            customerId = custId;
            balance = bal;
            transactions = new List<Transaction>();
        }

        public void Deposit(decimal currency)
        {
            if(currency < 0)
            {
                Console.WriteLine(" ** Cannot deposit negative values. **");
            }
            else
            {
                balance += decimal.Add(currency, 0.00M);
                Console.WriteLine(" * Current balance in account: " + accountNumber + " has changed to: " + balance);
            }

        }

        public void Withdraw(decimal currency)
        {
            if(balance < currency)
            {
                Console.WriteLine(" ** Insufficient credits on account. ** ");
            }
            else if(currency < 0)
            {
                Console.WriteLine(" ** Cannot withdraw a negative value. ** ");
            }
            else
            {
                balance -= decimal.Add(currency, 0.00M);
                Console.WriteLine(" * Current balance in account: " + accountNumber + " has changed to: " + balance);
            }
        }
    }
}
