using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Account
    {
        public int AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public decimal Interest { get; set; }
        public decimal DebtInterest { get; set; }
        public int Credit { get; set; }
        public List<Transaction> transactions;

        public Account(int accNum, int custId, decimal bal)
        {
            AccountNumber = accNum;
            CustomerId = custId;
            Balance = bal;
            Interest = 0.05M;
            Credit = 10000;
            transactions = new List<Transaction>();
        }

        public void SetCredit()
        {
            Console.WriteLine(" * Current credit for the account is: " + Credit + ". * ");
            Console.Write(" * Set credit to: ");
            string newCredit = Console.ReadLine();
            if(int.TryParse(newCredit, out int credit))
            {
                Credit = credit;
                Console.WriteLine(" * Credit set to: " + Credit + ". * ");
            }
        }

        public void SetInterest()
        {
            Console.WriteLine(" * Current interest for this account is: " + Interest + ". * ");
            Console.Write(" * Set interest to: ");
            string newInterest = Console.ReadLine();
            if(decimal.TryParse(newInterest, out decimal interest))
            {
                Interest = interest;
                Console.WriteLine(" * Interest set to: " + Interest + ". * ");
            }
        }

        public void Deposit(decimal currency)
        {
            if(currency < 0)
            {
                Console.WriteLine(" ** Cannot deposit negative values. **");
            }
            else
            {
                Balance += decimal.Add(currency, 0.00M);
                Console.WriteLine(" * Current balance in account: " + AccountNumber + ", has changed to: " + Balance);
            }
        }

        public void Withdraw(decimal currency)
        {
            if(Balance < 0)
            {
                Console.WriteLine(" * You cannot withdraw anymore from this account right now. * ");
            }
            else if(Balance < currency || (Balance - currency) > (-Credit))
            {
                Console.WriteLine(" ** Insufficient credits on account. ** ");
                Console.WriteLine(" ** Current balance: " + Balance + ", user tried to withdraw: " + decimal.Add(currency, 0.00M) + " ** ");
            }
            else if(currency < 0)
            {
                Console.WriteLine(" ** Cannot withdraw a negative value. ** ");
            }
            else
            {
                Balance -= decimal.Add(currency, 0.00M);
                Console.WriteLine(" * Current balance in account: " + AccountNumber + ", has changed to: " + Balance);
            }
        }
    }
}
