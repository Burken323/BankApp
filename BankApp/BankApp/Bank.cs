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
            Console.WriteLine("   |__________________________________________|   ");
            Console.WriteLine(" *************************************************");
            while (true)
            {
                string userInput = Console.ReadLine();
                if (userInput.Length >= 1)
                {

                    if (userInput[0].Equals('0'))
                    {
                        Console.WriteLine("  ** Saving data to file.. **  ");
                        db.SaveData();

                        Console.WriteLine("  ** Exiting program. **  ");
                        //Save data into different file and exit.
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    if (userInput[0].Equals('1'))
                    {
                        //Search/Choose customer.
                        Console.WriteLine(" * Search customer. *");
                        Console.Write(" * Name or zipcode: ");
                        string cust = Console.ReadLine();
                        var findCust = from customer in db.customers
                                    where customer.Value.organizationName.Contains(cust) || customer.Value.orgZipCode == cust
                                    select customer;
                        if (!cust.Equals(""))
                        {
                            foreach (var item in findCust)
                            {
                                Console.Write(item.Value.id + " | " + item.Value.organizationName + "\r\n");
                            }
                        }
                    }
                    if (userInput[0].Equals('2'))
                    {
                        //Show full customer info.
                        Console.WriteLine(" * Show customerimage. * ");
                        Console.Write(" * Customerid: ");
                        string cust = Console.ReadLine();
                        Console.WriteLine();
                        if (int.TryParse(cust, out int custID))
                        {
                            var findCust = from customer in db.customers
                                           where custID == customer.Value.id
                                           select customer;
                            foreach (var item in findCust)
                            {
                                Console.WriteLine("Customer ID: " + item.Value.id);
                                Console.WriteLine("Organization number: " + item.Value.organizationNumber);
                                Console.WriteLine("Name: " + item.Value.organizationName);
                                Console.WriteLine("Address: " + item.Value.orgAddress);
                            }
                            Console.WriteLine();
                            Console.WriteLine("Accounts: ");
                            var findAccounts = from account in db.accounts
                                               where account.Value.customerId == custID
                                               select account;
                            foreach (var item in findAccounts)
                            {
                                Console.WriteLine(item.Value.accountNumber + ": " + item.Value.balance);
                            }

                        }
                        else
                        {
                            Console.WriteLine("Could not find the customer you were looking for.");
                        }

                    }
                    if (userInput[0].Equals('3'))
                    {
                        //Create new customer.
                        Console.WriteLine(" * Create new customer. *");
                        db.AddCustomer();


                    }
                    if (userInput[0].Equals('4'))
                    {
                        //Remove customer from bank.
                        Console.WriteLine(" * Remove customer from bank. *");
                        Console.Write(" * Customerid: ");
                        string id = Console.ReadLine();
                        if(int.TryParse(id, out int custId))
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
                            Console.WriteLine("The customer ID only contains numbers.");
                        }

                    }
                    if (userInput[0].Equals('5'))
                    {
                        //Create new account.
                        Console.WriteLine(" * Create new account. * ");
                        db.AddAccount();


                    }
                    if (userInput[0].Equals('6'))
                    {
                        //Remove account from bank
                        Console.WriteLine(" * Remove account from bank. * ");
                        Console.Write(" * Customer ID: ");
                        string id = Console.ReadLine();
                        if(int.TryParse(id, out int custID))
                        {
                            int findCust = (from customer in db.customers
                                            where customer.Value.id == custID
                                            select customer).Count();
                            if(findCust == 1)
                            {
                                Console.Write(" * Account ID: ");
                                string acc = Console.ReadLine();
                                if (int.TryParse(acc, out int accID))
                                {
                                    var selAccount = (from account in db.accounts
                                                     where account.Value.accountNumber == accID
                                                     select account).ToList();
                                    db.RemoveAccount(selAccount[0].Value.accountNumber);
                                }
                            }
                        }

                    }
                    if (userInput[0].Equals('7'))
                    {
                        //Deposit.
                        Console.WriteLine(" * Deposit.");
                    }
                    if (userInput[0].Equals('8'))
                    {
                        //Withdraw.
                        Console.WriteLine(" * Withdraw. * ");
                    }
                    if (userInput[0].Equals('9'))
                    {
                        //Transfer.
                        Console.WriteLine(" * Transfer. *");
                    }
                    Console.WriteLine();
                }
            }
        }

        public void Withdraw()
        {
            throw new NotImplementedException();
        }

        public void Deposit()
        {
            throw new NotImplementedException();
        }

        


    }
}
