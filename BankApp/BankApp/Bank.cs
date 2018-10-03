using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Bank
    {
        public Database db;
        public Customer cs;

        public Bank()
        {
            db = new Database();
            cs = new Customer();
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
                        db.AddCustomer();
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
                        db.AddAccount();
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
                }
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
            db.GetData();
            Console.WriteLine(" ** We currently have: " + db.numberOfCust + " customers. **");
            Console.WriteLine(" ** We currently have: " + db.numberOfAcc + " accounts. **");
            var totalBalance = (from account in db.accounts.Values
                                select account.balance).Sum();
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
                if (db.customers.ContainsKey(custID))
                {
                    var findCust = (from customer in db.customers
                                    where customer.Value.id == custID
                                    select customer).Single();
                    var accounts = from account in db.accounts
                                   where custID == account.Value.customerId
                                   select account.Value;
                    Console.Write(" * Accounts: | ");
                    foreach (var item in accounts)
                    {
                        Console.Write(item.accountNumber + " | ");
                    }
                    Console.Write("\r\n");
                    Console.Write(" * Account ID: ");
                    string acc = Console.ReadLine();
                    if (int.TryParse(acc, out int accID))
                    {
                        if (db.accounts.ContainsKey(accID))
                        {
                            var findAcc = (from account in db.accounts
                                           where account.Value.accountNumber == accID && custID == account.Value.customerId
                                           select account.Value).Single();
                            Console.Write(" * Amount: ");
                            string amount = Console.ReadLine();
                            if (decimal.TryParse(amount, out decimal currency))
                            {
                                findAcc.Deposit(currency);
                                findAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.accountNumber, findAcc.accountNumber, decimal.Add(currency, 0.00M), findAcc.balance, "Deposit"));

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
                if (db.customers.ContainsKey(custID))
                {
                    var findCust = (from customer in db.customers
                                    where customer.Value.id == custID
                                    select customer).Single();
                    var accounts = from account in db.accounts
                                   where custID == account.Value.customerId
                                   select account.Value;
                    Console.Write(" * Accounts: | ");
                    foreach (var item in accounts)
                    {
                        Console.Write(item.accountNumber + " | ");
                    }
                    Console.Write("\r\n");
                    Console.Write(" * Account ID: ");
                    string acc = Console.ReadLine();
                    if (int.TryParse(acc, out int accID))
                    {
                        if (db.accounts.ContainsKey(accID))
                        {
                            var findAcc = (from account in db.accounts
                                           where account.Value.accountNumber == accID && custID == account.Value.customerId
                                           select account.Value).Single();
                            Console.Write(" * Amount: ");
                            string amount = Console.ReadLine();
                            if (decimal.TryParse(amount, out decimal currency))
                            {
                                findAcc.Withdraw(currency);
                                findAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.accountNumber, findAcc.accountNumber, decimal.Add(currency, 0.00M), findAcc.balance, "Withdrawal"));

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

        private void Transfer()
        {
            Console.WriteLine(" * Transfer. *");
            Console.Write(" * Customer ID: ");
            string cust = Console.ReadLine();
            if (int.TryParse(cust, out int custID))
            {
                if (db.customers.ContainsKey(custID))
                {
                    var findCust = (from customer in db.customers
                                    where customer.Value.id == custID
                                    select customer).Single();
                    var accounts = from account in db.accounts
                                   where custID == account.Value.customerId
                                   select account.Value;
                    Console.Write(" * Accounts: | ");
                    foreach (var item in accounts)
                    {
                        Console.Write(item.accountNumber + " | ");
                    }
                    Console.Write("\r\n");
                    Console.Write(" * From account ID: ");
                    string fromAcc = Console.ReadLine();
                    if (int.TryParse(fromAcc, out int fromAccID))
                    {
                        if (db.accounts.ContainsKey(fromAccID))
                        {
                            var findAcc = (from account in db.accounts
                                           where account.Value.accountNumber == fromAccID && custID == account.Value.customerId
                                           select account.Value).Single();
                            Console.Write(" * To account ID: ");
                            string toAcc = Console.ReadLine();
                            if (int.TryParse(toAcc, out int toAccID))
                            {
                                if (db.accounts.ContainsKey(toAccID))
                                {
                                    var findSecAcc = (from account in db.accounts
                                                      where account.Value.accountNumber == toAccID
                                                      select account.Value).Single();
                                    Console.Write(" * Amount: ");
                                    string amount = Console.ReadLine();
                                    if (decimal.TryParse(amount, out decimal currency))
                                    {
                                        if (currency < 0)
                                        {
                                            Console.WriteLine(" * Cannot transfer negative numbers. * ");
                                        }
                                        else
                                        {
                                            findAcc.balance -= currency;
                                            findSecAcc.balance += currency;
                                            findAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.accountNumber, findSecAcc.accountNumber, decimal.Add(currency, 0.00M), findAcc.balance, "Transfer"));
                                            findSecAcc.transactions.Add(new Transaction(DateTime.Now.ToString(), findAcc.accountNumber, findSecAcc.accountNumber, decimal.Add(currency, 0.00M), findSecAcc.balance, "Transfer"));
                                            Console.WriteLine();
                                            Console.WriteLine(" * Successfully transferred " + currency + " from " + findAcc.accountNumber + " to " + findSecAcc.accountNumber + " * ");
                                            Console.WriteLine();
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

        private void GetAccountImage()
        {
            Console.WriteLine(" * Accountimage * ");
            Console.Write(" * Account ID: ");
            string acc = Console.ReadLine();
            if (int.TryParse(acc, out int accID))
            {
                if (db.accounts.ContainsKey(accID))
                {
                    var findAcc = (from account in db.accounts
                                   where accID == account.Value.accountNumber
                                   select account.Value).Single();
                    Console.WriteLine();
                    Console.WriteLine(" * Account ID: " + findAcc.accountNumber);
                    Console.WriteLine(" * Customer ID: " + findAcc.customerId);
                    Console.WriteLine(" * Balance: " + findAcc.balance);
                    Console.WriteLine(" * Transactions: ");
                    Console.WriteLine();
                    var getTransfers = (from transaction in findAcc.transactions
                                        select transaction).ToList();
                    if (getTransfers.Count != 0)
                    {
                        foreach (var item in getTransfers)
                        {
                            Console.WriteLine(" ** " + item.type + " ** ");
                            Console.WriteLine(" * Date: " + item.date);
                            Console.WriteLine(" * Sender: " + item.sender);
                            Console.WriteLine(" * Reciever: " + item.reciever);
                            Console.WriteLine(" * Amount: " + item.amount);
                            Console.WriteLine(" * Current balance: " + item.currentBalance);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine(" ** No recent activity. ** ");
                    }
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

        private void RemoveAccFromBank()
        {
            Console.WriteLine(" * Remove account from bank. * ");
            Console.Write(" * Customer ID: ");
            string id = Console.ReadLine();
            if (int.TryParse(id, out int custID))
            {
                int findCust = (from customer in db.customers
                                where customer.Value.id == custID
                                select customer).Count();
                if (findCust == 1)
                {
                    Console.Write(" * Account ID: ");
                    string acc = Console.ReadLine();
                    if (int.TryParse(acc, out int accID))
                    {
                        var selAccount = (from account in db.accounts
                                          where account.Value.accountNumber == accID
                                          select account).Single();
                        db.RemoveAccount(selAccount.Value.accountNumber);
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
                int accountNum = (from account in db.accounts
                                  where account.Value.customerId == custId
                                  select account).Count();

                if (accountNum == 0)
                {
                    db.RemoveCustomer(custId);
                }
                else
                {
                    Console.WriteLine("That customer still has accounts with us.");
                }
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
            string cust = Console.ReadLine();
            Console.WriteLine();
            if (int.TryParse(cust, out int custID))
            {
                if (db.customers.ContainsKey(custID))
                {
                    var foundCust = (from customer in db.customers
                                    where custID == customer.Value.id
                                    select customer.Value).Single();
                    var findAccounts = from account in db.accounts
                                       where account.Value.customerId == custID
                                       select account;
                    PrintCustomerImage(findAccounts, foundCust);
                }
                else if(db.accounts.ContainsKey(custID))
                {
                    var acc = db.accounts[custID];
                    var findAccounts = from account in db.accounts
                                       where account.Value.customerId == acc.customerId
                                       select account;
                    var foundCust = (from customer in db.customers
                                     where acc.customerId == customer.Value.id
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
            Console.WriteLine("Customer ID: " + foundCust.id);
            Console.WriteLine("Organization number: " + foundCust.organizationNumber);
            Console.WriteLine("Name: " + foundCust.organizationName);
            Console.WriteLine("Address: " + foundCust.orgAddress);

            Console.WriteLine();
            Console.WriteLine("Accounts: ");

            foreach (var item in findAccounts)
            {
                Console.WriteLine(item.Value.accountNumber + ": " + item.Value.balance);
            }
        }

        private void SaveAndExit()
        {
            Console.WriteLine("  ** Saving data to file.. **  ");
            db.SaveData();

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
            var findCust = from customer in db.customers
                           where customer.Value.organizationName.Contains(cust) || customer.Value.orgZipCode == cust
                           select customer;
            if (!(findCust.Count() < 1))
            {
                Console.WriteLine();
                foreach (var item in findCust)
                {
                    Console.Write(item.Value.id + " | " + item.Value.organizationName + "\r\n");
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
