using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Bank
    {
        public Database DataBase;
        public DateTime StartUp;

        public Bank()
        {
            DataBase = new Database();
            StartUp = DateTime.Now;
        }

        public void BankOpen()
        {
            PrintMenu();
            while (true)
            {
                string userInput = Console.ReadLine();
                if (userInput.Length >= 1)
                {

                    if (userInput.Equals("0"))
                    {
                        //Save the data and exit the application.
                        SaveAndExit();
                    }
                    if (userInput.Equals("1"))
                    {
                        //Search customer.
                        SearchCustomer();
                    }
                    if (userInput.Equals("2"))
                    {
                        //Show full customer info.
                        ShowCustomerImage();
                    }
                    if (userInput.Equals("3"))
                    {
                        //Create new customer.
                        Console.WriteLine(" * Create new customer. *");
                        DataBase.AddCustomer();
                    }
                    if (userInput.Equals("4"))
                    {
                        //Remove customer from bank.
                        RemoveCustFromBank();
                    }
                    if (userInput.Equals("5"))
                    {
                        //Create new account.
                        Console.WriteLine(" * Create new account. * ");
                        DataBase.AddAccount();
                    }
                    if (userInput.Equals("6"))
                    {
                        //Remove account from bank
                        RemoveAccFromBank();
                    }
                    if (userInput.Equals("7"))
                    {
                        //Deposit.
                        DepositFromAccount();
                    }
                    if (userInput.Equals("8"))
                    {
                        //Withdraw.
                        WithdrawFromAccount();
                    }
                    if (userInput.Equals("9"))
                    {
                        //Transfer.
                        Transfer();
                    }
                    if (userInput.Equals("10"))
                    {
                        //Get the image for that account.
                        GetAccountImage();
                    }
                    if (userInput.Equals("11"))
                    {
                        //Set the interest for an account.
                        SetInterestForAccount();
                    }
                    if (userInput.Equals("12"))
                    {
                        //Calculate and add interest to accounts.
                        CalculateAndAddInterestToAccounts();
                    }
                    if (userInput.Equals("13"))
                    {
                        //Set credit for the account.
                        SetCreditForAccount();
                    }
                }
            }
        }

        private void SetCreditForAccount()
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

        private void CalculateAndAddInterestToAccounts()
        {
            var getAccounts = from account in DataBase.accounts
                             select account.Value;
            foreach (var item in getAccounts)
            {
                item.Balance += item.Balance * item.Interest;
                item.transactions.Add(new Transaction(DateTime.Now.ToString(), item.AccountNumber,
                                                            item.AccountNumber, decimal.Add((item.Balance * item.Interest), 0.00M), 
                                                                item.Balance, "Interest"));
            }
            Console.WriteLine(" * Interest added to all accounts. * ");
            Console.WriteLine();

        }

        private void SetInterestForAccount()
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

        private void PrintMenu()
        {
            Console.WriteLine("                                             ");
            Console.WriteLine(" ************************************************* ");
            Console.WriteLine("     $$$//$$$$$$$$$$$$$$//$$$$$$$$$$$$$$$//$   ");
            Console.WriteLine("     $$//WELCOME TO THE//BANK OF GABRIEL//$$   ");
            Console.WriteLine("     $//$$$$$$$$$$$$$$//$$$$$$$$$$$$$$$//$$$   ");
            Console.WriteLine(" ************************************************* ");
            Console.WriteLine("                                             ");
            Console.WriteLine(" ** Getting customer data...         ** ");
            DataBase.GetData();
            Console.WriteLine(" ** We currently have: " + DataBase.CustomerCount + " customers. **");
            Console.WriteLine(" ** We currently have: " + DataBase.AccountCount + " accounts. **");
            var totalBalance = (from account in DataBase.accounts.Values
                                select account.Balance).Sum();
            Console.WriteLine(" ** Total balance: " + totalBalance + ".        **");
            Console.WriteLine(" *************************************************");
            Console.WriteLine("    __________________________________________    ");
            Console.WriteLine("   | Main menu                                |   ");
            Console.WriteLine("   |------------------------------------------|   ");
            Console.WriteLine("   |[0]  Save and exit.                       |   ");
            Console.WriteLine("   |[1]  Search for customer.                 |   ");
            Console.WriteLine("   |[2]  Show customerimage.                  |   ");
            Console.WriteLine("   |[3]  Create customer.                     |   ");
            Console.WriteLine("   |[4]  Remove customer.                     |   ");
            Console.WriteLine("   |[5]  Create account.                      |   ");
            Console.WriteLine("   |[6]  Remove account.                      |   ");
            Console.WriteLine("   |[7]  Deposit.                             |   ");
            Console.WriteLine("   |[8]  Withdraw.                            |   ");
            Console.WriteLine("   |[9]  Transfer.                            |   ");
            Console.WriteLine("   |[10] Show accountimage.                   |   ");
            Console.WriteLine("   |[11] Change interest.                     |   ");
            Console.WriteLine("   |[12] Calculate interest.                  |   ");
            Console.WriteLine("   |[13] Credit and debtinterest.             |   ");
            Console.WriteLine("   |__________________________________________|   ");
            Console.WriteLine(" *************************************************");
        }

        private void DepositFromAccount()
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

        private void WithdrawFromAccount()
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

        private void Transfer()
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
                                        if (findAcc.Balance < 0)
                                        {
                                            Console.WriteLine(" * You cannot withdraw anymore from this account right now. * ");
                                        }
                                        else if (currency < 0)
                                        {
                                            Console.WriteLine(" * Cannot transfer negative numbers. * ");
                                        }
                                        else if ((findAcc.Balance - currency) < (-findAcc.Credit))
                                        {
                                            Console.WriteLine(" ** Insufficient credits on account. ** ");
                                            Console.WriteLine(" ** Current balance: " + findAcc.Balance + ", user tried to withdraw: " + decimal.Add(currency, 0.00M) + " ** ");
                                        }
                                        else if (findAcc.Balance < currency)
                                        {
                                            Console.WriteLine(" * Insufficient funds on account: " + findAcc.AccountNumber + ". * ");
                                            Console.WriteLine(" * Current balance: " + findAcc.Balance + ". * ");
                                        }
                                        else
                                        { 
                                            TransferToAcc(findAcc, findSecAcc, currency);
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

        private static void TransferToAcc(Account findAcc, Account findSecAcc, decimal currency)
        {
            findAcc.Balance -= currency;
            findSecAcc.Balance += currency;
            findAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber, 
                                        findSecAcc.AccountNumber, decimal.Add(currency, 0.00M), findAcc.Balance, "Transfer"));
            findSecAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.AccountNumber, 
                                        findSecAcc.AccountNumber, decimal.Add(currency, 0.00M), findSecAcc.Balance, "Transfer"));
            Console.WriteLine();
            Console.WriteLine(" * Successfully transferred " + currency + " from " + findAcc.AccountNumber + " to " + findSecAcc.AccountNumber + " * ");
            Console.WriteLine();
        }

        private void GetAccountImage()
        {
            Console.WriteLine(" * Accountimage * ");
            Console.Write(" * Account ID: ");
            string acc = Console.ReadLine();
            if (int.TryParse(acc, out int accID))
            {
                if (DataBase.accounts.ContainsKey(accID))
                {
                    var findAcc = (from account in DataBase.accounts
                                   where accID == account.Value.AccountNumber
                                   select account.Value).Single();
                    PrintAccountAndTransactions(findAcc);
                }
                else
                {
                    Console.WriteLine(" * Account doesn't exist. *");
                }

            }
            else
            {
                Console.WriteLine(" * Input invalid. * ");
            }
        }

        private static void PrintAccountAndTransactions(Account findAcc)
        {
            Console.WriteLine();
            Console.WriteLine(" * Account ID: " + findAcc.AccountNumber);
            Console.WriteLine(" * Customer ID: " + findAcc.CustomerId);
            Console.WriteLine(" * Balance: " + findAcc.Balance);
            Console.WriteLine(" * Transactions: ");
            Console.WriteLine();
            var getTransfers = (from transaction in findAcc.transactions
                                select transaction).ToList();
            if (getTransfers.Count != 0)
            {
                foreach (var item in getTransfers)
                {
                    CheckAndPrintTypeOfTransaction(item);
                }
            }
            else
            {
                Console.WriteLine(" ** No recent activity. ** ");
            }
        }

        private static void CheckAndPrintTypeOfTransaction(Transaction item)
        {
            if (item.Sender == item.Reciever)
            {
                Console.WriteLine(" ** " + item.Type + " ** ");
                Console.WriteLine(" * Date: " + item.Date);
                Console.WriteLine(" * Amount: " + item.Amount);
                Console.WriteLine(" * Current balance: " + item.CurrentBalance);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(" ** " + item.Type + " ** ");
                Console.WriteLine(" * Date: " + item.Date);
                Console.WriteLine(" * Sender: " + item.Sender);
                Console.WriteLine(" * Reciever: " + item.Reciever);
                Console.WriteLine(" * Amount: " + item.Amount);
                Console.WriteLine(" * Current balance: " + item.CurrentBalance);
                Console.WriteLine();
            }
        }

        private void RemoveAccFromBank()
        {
            Console.WriteLine(" * Remove account from bank. * ");
            Console.Write(" * Customer ID: ");
            string id = Console.ReadLine();
            if (int.TryParse(id, out int custID))
            {
                int findCust = (from customer in DataBase.customers
                                where customer.Value.Id == custID
                                select customer).Count();
                if (findCust == 1)
                {
                    Console.Write(" * Account ID: ");
                    string acc = Console.ReadLine();
                    if (int.TryParse(acc, out int accID))
                    {
                        var selAccount = (from account in DataBase.accounts
                                          where account.Value.AccountNumber == accID
                                          select account).Single();
                        DataBase.RemoveAccount(selAccount.Value.AccountNumber);
                    }
                    else
                    {
                        Console.WriteLine(" * Account not found. * ");
                    }
                }
            }
            else
            {
                Console.WriteLine(" * Customer not found. * ");
            }
        }

        private void RemoveCustFromBank()
        {
            Console.WriteLine(" * Remove customer from bank. *");
            Console.Write(" * Customerid: ");
            string id = Console.ReadLine();
            if (int.TryParse(id, out int custId))
            {
                DataBase.RemoveCustomer(custId);
            }
            else
            {
                Console.WriteLine(" * Customer not found. * ");
            }
        }

        private void ShowCustomerImage()
        {
            Console.WriteLine(" * Show customerimage. * ");
            Console.Write(" * Customer ID or account ID: ");
            string custOrAcc = Console.ReadLine();
            Console.WriteLine();
            if (int.TryParse(custOrAcc, out int custOrAccID))
            {
                if (DataBase.customers.ContainsKey(custOrAccID))
                {
                    var foundCust = (from customer in DataBase.customers
                                    where custOrAccID == customer.Value.Id
                                    select customer.Value).Single();
                    var findAccounts = from account in DataBase.accounts
                                       where account.Value.CustomerId == custOrAccID
                                       select account;
                    PrintCustomerImage(findAccounts, foundCust);
                }
                else if(DataBase.accounts.ContainsKey(custOrAccID))
                {
                    var acc = DataBase.accounts[custOrAccID];
                    var findAccounts = from account in DataBase.accounts
                                       where account.Value.CustomerId == acc.CustomerId
                                       select account;
                    var foundCust = (from customer in DataBase.customers
                                     where acc.CustomerId == customer.Value.Id
                                     select customer.Value).Single();
                    PrintCustomerImage(findAccounts, foundCust);
                }
                else
                {
                    Console.WriteLine(" * Could not find customer. * ");
                }

            }
            else
            {
                Console.WriteLine("Could not find the customer you were looking for.");
            }
        }

        private static void PrintCustomerImage(IEnumerable<KeyValuePair<int, Account>> findAccounts, Customer foundCust)
        {
            Console.WriteLine("Customer ID: " + foundCust.Id);
            Console.WriteLine("Organization number: " + foundCust.OrganizationNumber);
            Console.WriteLine("Name: " + foundCust.OrganizationName);
            Console.WriteLine("Address: " + foundCust.OrganizationAddress);

            Console.WriteLine();
            Console.WriteLine("Accounts: ");

            decimal totalBalance = 0;
            foreach (var item in findAccounts)
            {
                Console.WriteLine(item.Value.AccountNumber + ": " + item.Value.Balance);
                totalBalance += item.Value.Balance;
            }
            Console.WriteLine("Total balance on all accounts: " + totalBalance + ".");
            Console.WriteLine();
        }

        private void SaveAndExit()
        {
            Console.WriteLine("  ** Saving data to file.. **  ");
            DataBase.SaveData();

            Console.WriteLine("  ** Exiting program. **  ");
            //Save data into different file and exit.
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void SearchCustomer()
        {
            Console.WriteLine(" * Search customer. *");
            Console.Write(" * Name or zipcode: ");
            string cust = Console.ReadLine();
            var findCust = from customer in DataBase.customers
                           where (customer.Value.OrganizationName.Contains(cust) || 
                                    customer.Value.OrganizationCity.Contains(cust)) && !String.IsNullOrWhiteSpace(cust)
                           select customer;
            if (!(findCust.Count() < 1))
            {
                Console.WriteLine();
                foreach (var item in findCust)
                {
                    Console.Write(item.Value.Id + " | " + item.Value.OrganizationName + "\r\n");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(" * Could not find any customers. * ");
            }
        }
    }
}
