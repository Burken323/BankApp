using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Bank
    {
        public Database DataBase;

        public Bank()
        {
            DataBase = new Database();
        }

        public void BankOpen()
        {
            var menu = new Menu(DataBase);
            menu.PrintMenu();
            while (true)
            {
                menu.CheckInput();
            }
        }

        public void SetDebtInterestForAccount()
        {
            Console.WriteLine(" * Debtinterest. * ");
            Console.Write(" * Account ID: ");
            string acc = Console.ReadLine();
            if (int.TryParse(acc, out int accID))
            {
                if (DataBase.accounts.ContainsKey(accID))
                {
                    DataBase.accounts[accID].SetDebtInterest();
                }
                else
                {
                    Console.WriteLine(" * Could not find that account. * ");
                }
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
            }
        }

        public void SetCreditForAccount()
        {
            Console.WriteLine(" * Credit. * ");
            Console.Write(" * Account ID: ");
            string acc = Console.ReadLine();
            if (int.TryParse(acc, out int accID))
            {
                if (DataBase.accounts.ContainsKey(accID))
                {
                    DataBase.accounts[accID].SetCredit();
                }
                else
                {
                    Console.WriteLine(" * Could not find that account. * ");
                }
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
            }
        }

        public void CalculateAndAddInterestToAccounts()
        {
            //TODO: Need to double check that the interest and debtinterest is working as intended...
            var getAccounts = from account in DataBase.accounts
                             select account.Value;
            foreach (var item in getAccounts)
            {
                decimal balance = 0;
                
                if(item.Balance < 0)
                {
                    balance = -item.Balance;
                    item.Balance -= decimal.Round(balance * (item.Interest - item.DebtInterest), 4);
                    item.transactions.Add(new Transaction(DateTime.Now.ToString(), item.AccountNumber,
                                                                item.AccountNumber, -balance * item.Interest,
                                                                    item.Balance, "Interest"));
                }
                else
                {
                    balance = item.Balance;
                    item.Balance += decimal.Round(balance * (item.Interest - item.DebtInterest), 4);
                    item.transactions.Add(new Transaction(DateTime.Now.ToString(), item.AccountNumber,
                                                                item.AccountNumber, balance * item.Interest,
                                                                    item.Balance, "Interest"));
                }
            }
            Console.WriteLine(" * Interest added to all accounts. * ");
            Console.WriteLine();

        }

        public void SetInterestForAccount()
        {
            Console.WriteLine(" * Interest. * ");
            Console.Write(" * Account ID: ");
            string acc = Console.ReadLine();
            if (int.TryParse(acc, out int accID))
            {
                if (DataBase.accounts.ContainsKey(accID))
                {
                    DataBase.accounts[accID].SetInterest();
                }
                else
                {
                    Console.WriteLine(" * Could not find that account. * ");
                }
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
            }
        }

        public void DepositFromAccount()
        {
            Console.WriteLine(" * Deposit. *");
            Console.Write(" * Customer ID: ");
            string cust = Console.ReadLine();
            if (int.TryParse(cust, out int custID))
            {
                if (DataBase.customers.ContainsKey(custID))
                {
                    var findCust = (from customer in DataBase.customers
                                    where customer.Value.Id == custID
                                    select customer).Single();
                    GetAccountsForCustomer(custID);
                    Console.Write(" * Account ID: ");
                    string acc = Console.ReadLine();
                    if (int.TryParse(acc, out int accID))
                    {
                        if (DataBase.accounts.ContainsKey(accID))
                        {
                            var findAcc = (from account in DataBase.accounts
                                           where account.Value.AccountNumber == accID && custID == account.Value.CustomerId
                                           select account.Value).Single();
                            Console.Write(" * Amount: ");
                            string amount = Console.ReadLine();
                            if (decimal.TryParse(amount, out decimal currency))
                            {
                                findAcc.Deposit(currency);
                                findAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber, 
                                                            findAcc.AccountNumber, decimal.Add(currency, 0.00M), findAcc.Balance, "Deposit"));
                            }
                            else
                            {
                                Console.WriteLine(" * Invalid input. * ");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" * Could not find account. * ");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" * Invalid input. * ");
                    }
                }
                else
                {
                    Console.WriteLine(" * Could not find that customer. * ");
                }
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
            }
        }

        public void WithdrawFromAccount()
        {
            Console.WriteLine(" * Withdraw. *");
            Console.Write(" * Customer ID: ");
            string cust = Console.ReadLine();
            if (int.TryParse(cust, out int custID))
            {
                if (DataBase.customers.ContainsKey(custID))
                {
                    var findCust = (from customer in DataBase.customers
                                    where customer.Value.Id == custID
                                    select customer).Single();
                    GetAccountsForCustomer(custID);
                    Console.Write(" * Account ID: ");
                    string acc = Console.ReadLine();
                    if (int.TryParse(acc, out int accID))
                    {
                        if (DataBase.accounts.ContainsKey(accID))
                        {
                            var findAcc = (from account in DataBase.accounts
                                           where account.Value.AccountNumber == accID && custID == account.Value.CustomerId
                                           select account.Value).Single();
                            Console.Write(" * Amount: ");
                            string amount = Console.ReadLine();
                            if (decimal.TryParse(amount, out decimal currency))
                            {
                                if (findAcc.Withdraw(currency))
                                {
                                    findAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber,
                                                                findAcc.AccountNumber, decimal.Add(currency, 0.00M), findAcc.Balance, "Withdrawal"));
                                }
                            }
                            else
                            {
                                Console.WriteLine(" * Invalid input. * ");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" * Could not find account. * ");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" * Invalid input. * ");
                    }
                }
                else
                {
                    Console.WriteLine(" * Could not find that customer. * ");
                }
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
            }
        }

        private void GetAccountsForCustomer(int custID)
        {
            var accounts = from account in DataBase.accounts
                           where custID == account.Value.CustomerId
                           select account.Value;
            Console.WriteLine();
            Console.Write(" * Accounts: | ");
            foreach (var item in accounts)
            {
                Console.Write(item.AccountNumber + " | ");
            }
            Console.Write("* \r\n");
            Console.WriteLine();
        }

        public void Transfer()
        {
            Console.WriteLine(" * Transfer. *");
            Console.Write(" * Customer ID: ");
            string cust = Console.ReadLine();
            if (int.TryParse(cust, out int custID))
            {
                if (DataBase.customers.ContainsKey(custID))
                {
                    var findCust = (from customer in DataBase.customers
                                    where customer.Value.Id == custID
                                    select customer).Single();
                    GetAccountsForCustomer(custID);
                    Console.Write(" * From account ID: ");
                    string fromAcc = Console.ReadLine();
                    if (int.TryParse(fromAcc, out int fromAccID))
                    {
                        if (DataBase.accounts.ContainsKey(fromAccID))
                        {
                            var findAcc = (from account in DataBase.accounts
                                           where account.Value.AccountNumber == fromAccID && custID == account.Value.CustomerId
                                           select account.Value).Single();
                            Console.Write(" * To account ID: ");
                            string toAcc = Console.ReadLine();
                            if (int.TryParse(toAcc, out int toAccID))
                            {
                                if (DataBase.accounts.ContainsKey(toAccID))
                                {
                                    var findSecAcc = (from account in DataBase.accounts
                                                      where account.Value.AccountNumber == toAccID
                                                      select account.Value).Single();
                                    Console.Write(" * Amount: ");
                                    string amount = Console.ReadLine();
                                    if (decimal.TryParse(amount, out decimal currency))
                                    {
                                        CheckCreditForTransfer(findAcc, findSecAcc, currency);

                                    }
                                    else
                                    {
                                        Console.WriteLine(" * Invalid input. * ");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(" * Could not find that account. * ");
                                }
                            }
                            else
                            {
                                Console.WriteLine(" * Invalid input. * ");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" * Could not find that account. * ");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" * Invalid input. * ");
                    }
                }
                else
                {
                    Console.WriteLine(" * Could not find that customer. * ");
                }
            }
            else
            {
                Console.WriteLine(" * Invalid input. * ");
            }
        }

        public void CheckCreditForTransfer(Account findAcc, Account findSecAcc, decimal currency)
        {
            if (currency < 0)
            {
                Console.WriteLine(" * Cannot transfer negative numbers. * ");
            }
            else if ((findAcc.Balance - currency) < (-findAcc.Credit))
            {
                Console.WriteLine(" ** Insufficient credits on account. ** ");
                Console.WriteLine(" ** Current balance: " + findAcc.Balance + ", user tried to withdraw: " +
                                        decimal.Add(currency, 0.00M) + " ** ");
            }
            else
            {
                TransferToAcc(findAcc, findSecAcc, currency);
            }
        }

        private void TransferToAcc(Account findAcc, Account findSecAcc, decimal currency)
        {

            findAcc.Balance -= decimal.Add(currency, 0.00M);
            findSecAcc.Balance += decimal.Add(currency, 0.00M);
            if(findAcc.Balance < 0)
            {
                findAcc.DebtInterest = 0.05M / 365;
                if (findAcc.Interest - findAcc.DebtInterest > 0)
                {
                    findAcc.Interest = findAcc.Interest - findAcc.DebtInterest;
                    Console.WriteLine(" * Current balance in account: " + findAcc.AccountNumber + ", has changed to: " + findAcc.Balance);
                }
                else
                {
                    Console.WriteLine(" ** ERROR! DebtInterest cannot be greater than the interest. ** ");
                }
            }
            if(findSecAcc.Balance > 0)
            {
                findSecAcc.DebtInterest = 0;
                findSecAcc.Interest = 0.25M / 365;
            }
            findAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber, 
                                        findSecAcc.AccountNumber, decimal.Add(currency, 0.00M), findAcc.Balance, "Transfer"));
            findSecAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber, 
                                        findSecAcc.AccountNumber, decimal.Add(currency, 0.00M), findSecAcc.Balance, "Transfer"));
            Console.WriteLine();
            Console.WriteLine(" * Successfully transferred " + currency + " from " + findAcc.AccountNumber + " to " + findSecAcc.AccountNumber + " * ");
            Console.WriteLine();
        }

        public void SaveAndExit()
        {
            Console.WriteLine("  ** Saving data to file.. **  ");
            DataBase.SaveDataBase();

            Console.WriteLine("  ** Exiting program. **  ");
            //Save data into different file and exit.
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
