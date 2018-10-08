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
            var getAccounts = from account in DataBase.accounts
                             select account.Value;
            foreach (var item in getAccounts)
            {
                decimal balance = 0;
                
                if(item.Balance < 0)
                {
                    balance = -item.Balance;
                    item.Balance += decimal.Round(balance * item.Interest, 4);
                    item.Balance += decimal.Round(-balance * item.DebtInterest, 4);
                    Transaction transaction = new Transaction(DateTime.Now.ToString(), item.AccountNumber, item.AccountNumber,
                                                                decimal.Round((balance * item.Interest) + (-balance * item.DebtInterest), 4),
                                                                    item.Balance, "Interest");
                    item.transactions.Add(transaction);
                    FileManager.SaveTransaction(transaction);
                }
                else
                {
                    balance = item.Balance;
                    item.Balance += decimal.Round(balance * item.Interest, 4);
                    Transaction transaction = new Transaction(DateTime.Now.ToString(), item.AccountNumber,
                                                                item.AccountNumber, decimal.Round(balance * item.Interest, 4),
                                                                    item.Balance, "Interest");
                    item.transactions.Add(transaction);
                    FileManager.SaveTransaction(transaction);
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
            if (InputManager.VerifyCustomer(DataBase, cust, out int custID))
            {
                var findCust = (from customer in DataBase.customers
                                where customer.Value.Id == custID
                                select customer).Single();
                GetAccountsForCustomer(custID);
                Console.Write(" * Account ID: ");
                string acc = Console.ReadLine();
                if(InputManager.VerifyAccount(DataBase, acc, out int accID))
                {
                    var findAcc = (from account in DataBase.accounts
                                   where account.Value.AccountNumber == accID && custID == account.Value.CustomerId
                                   select account.Value).Single();
                    Console.Write(" * Amount: ");
                    string amount = Console.ReadLine();
                    if (InputManager.VerifyCurrency(DataBase, amount, out decimal currency))
                    {
                        findAcc.Deposit(currency);
                        Transaction transaction = new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber,
                                                    findAcc.AccountNumber, decimal.Add(currency, 0.00M), findAcc.Balance, "Deposit");
                        findAcc.transactions.Add(transaction);
                        FileManager.SaveTransaction(transaction);
                    }
                }
            }
        }

        public void WithdrawFromAccount()
        {
            Console.WriteLine(" * Withdraw. *");
            Console.Write(" * Customer ID: ");
            string cust = Console.ReadLine();
            if (InputManager.VerifyCustomer(DataBase, cust, out int custID))
            {
                var findCust = (from customer in DataBase.customers
                                where customer.Value.Id == custID
                                select customer).Single();
                GetAccountsForCustomer(custID);
                Console.Write(" * Account ID: ");
                string acc = Console.ReadLine();
                if (InputManager.VerifyAccount(DataBase, acc, out int accID))
                {
                    var findAcc = (from account in DataBase.accounts
                                    where account.Value.AccountNumber == accID && custID == account.Value.CustomerId
                                    select account.Value).Single();
                    Console.Write(" * Amount: ");
                    string amount = Console.ReadLine();
                    if (InputManager.VerifyCurrency(DataBase, amount, out decimal currency))
                    {
                        if (findAcc.Withdraw(currency))
                        {
                            Transaction transaction = new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber,
                                                        findAcc.AccountNumber, decimal.Add(currency, 0.00M), findAcc.Balance, "Withdrawal");
                            findAcc.transactions.Add(transaction);
                            FileManager.SaveTransaction(transaction);
                        }
                    }
                } 
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
            if (InputManager.VerifyCustomer(DataBase, cust, out int custID))
            {
                var findCust = (from customer in DataBase.customers
                                where customer.Value.Id == custID
                                select customer).Single();
                GetAccountsForCustomer(custID);
                Console.Write(" * From account ID: ");
                string fromAcc = Console.ReadLine();
                if (InputManager.VerifyAccount(DataBase, fromAcc, out int fromAccID))
                {
                    var findAcc = (from account in DataBase.accounts
                                    where account.Value.AccountNumber == fromAccID && custID == account.Value.CustomerId
                                    select account.Value).Single();
                    Console.Write(" * To account ID: ");
                    string toAcc = Console.ReadLine();
                    if (InputManager.VerifyAccount(DataBase, toAcc, out int toAccID))
                    {
                        var findSecAcc = (from account in DataBase.accounts
                                            where account.Value.AccountNumber == toAccID
                                            select account.Value).Single();
                        Console.Write(" * Amount: ");
                        string amount = Console.ReadLine();
                        if (InputManager.VerifyCurrency(DataBase, amount, out decimal currency))
                        {
                            CheckCreditForTransfer(findAcc, findSecAcc, currency);

                        }
                    }
                }   
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
                findAcc.DebtInterest = 0.3M / 365;
                if (findAcc.Interest - findAcc.DebtInterest > 0)
                {
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
