using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public decimal YearInterest { get; set; }
        public decimal Interest { get; set; }
        public decimal DebtInterest { get; set; }
        public int Credit { get; set; }
        public List<Transaction> transactions;

        public Account(int accNum, int custId, decimal bal)
        {
            AccountNumber = accNum;
            CustomerId = custId;
            Balance = bal;
            YearInterest = 0.25M;
            Interest = 0.25M / 365;
            Credit = 10000;
            DebtInterest = 0;
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
        public void SetDebtInterest()
        {
            Console.WriteLine(" * Current debtinterest for the day on this account is: " + DebtInterest + ". * ");
            Console.Write(" * Set debtinterest for the day to: ");
            string newDebtInterest = Console.ReadLine();
            if (decimal.TryParse(newDebtInterest, out decimal debtInterest))
            {
                if ((debtInterest / 100) < YearInterest)
                {
                    DebtInterest = (debtInterest/100)/365;
                    Console.WriteLine(" * Debtinterest set to: " + debtInterest + ". * ");
                }
                else
                {
                    Console.WriteLine(" * Debtinterest must be smaller than the interest for the account. * ");
                }
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
            }
        }

        public void SetInterest()
        {
            Console.WriteLine(" * Current interest for the day on this account is: " + Interest + ". * ");
            Console.WriteLine(" * Current interest for the year on this account is: " + YearInterest + ". * ");
            Console.Write(" * Set year interest to: ");
            string newInterest = Console.ReadLine();
            if (decimal.TryParse(newInterest, out decimal interest))
            {
                YearInterest = interest/100;
                Interest = YearInterest / 365;
                Console.WriteLine(" * Year interest set to: " + interest + "%. * ");
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
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
                if (Balance > 0)
                {
                    DebtInterest = 0;
                    Interest = 0.25M / 365;
                }
                Console.WriteLine(" * Current balance in account: " + AccountNumber + ", has changed to: " + Balance);
            }
        }

        public bool Withdraw(decimal currency)
        {
            if (currency < 0)
            {
                Console.WriteLine(" ** Cannot withdraw a negative value. ** ");
                return false;
            }
            else if((Balance - currency) < (-Credit))
            {
                Console.WriteLine(" ** Insufficient credits on account. ** ");
                Console.WriteLine(" ** Current balance: " + Balance + ", user tried to withdraw: " + decimal.Add(currency, 0.00M) + " ** ");
                return false;
            }
            
            else
            {
                Balance -= decimal.Add(currency, 0.00M);
                if(Balance < 0)
                {
                    DebtInterest = 0.05M / 365;
                    if(Interest - DebtInterest > 0)
                    {
                        Interest = Interest - DebtInterest;
                        Console.WriteLine(" * Current balance in account: " + AccountNumber + ", has changed to: " + Balance);
                    }
                    else
                    {
                        Console.WriteLine(" ** ERROR! DebtInterest cannot be greater than the interest. ** ");
                    }
                }
                else
                {
                    Console.WriteLine(" * Current balance in account: " + AccountNumber + ", has changed to: " + Balance);
                }
                return true;
            }
        }
    }
}
