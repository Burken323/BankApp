using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Database
    {
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }
        public Dictionary<int, Customer> customers;
        public Dictionary<int, Account> accounts;

        public Database()
        {
            customers = new Dictionary<int, Customer>();
            accounts = new Dictionary<int, Account>();
        }

        public void GetData()
        {
            using (var data = new StreamReader("bankdata.txt"))
            {
                string[] line = data.ReadLine().Split(';');
                
                if(int.TryParse(line[0], out int numOfCust))
                {
                    CustomerCount = numOfCust;
                    line = GetCustomers(data, line, numOfCust);
                }
                else
                {
                    Console.WriteLine("Error...");
                }

                line = data.ReadLine().Split(';');

                if (int.TryParse(line[0], out int numOfAcc))
                {
                    AccountCount = numOfAcc;
                    line = GetAccounts(data, line, numOfAcc);
                }
                else
                {
                    Console.WriteLine("Error...");
                }
            }
        }

        private string[] GetCustomers(StreamReader data, string[] line, int numOfCust)
        {
            for (int c = 0; c < numOfCust; c++)
            {
                line = data.ReadLine().Split(';');
                int tempId = int.Parse(line[0]);
                Customer cust = new Customer(tempId, line[1], line[2], line[3], line[4], line[5], line[6], line[7], line[8]);
                customers.Add(tempId, cust);
            }
            return line;
        }

        private string[] GetAccounts(StreamReader data, string[] line, int numOfAcc)
        {
            for (int c = 0; c < numOfAcc; c++)
            {
                line = data.ReadLine().Split(';');
                int tempId = int.Parse(line[0]);
                Account acc = new Account(tempId, int.Parse(line[1]), decimal.Parse(line[2], CultureInfo.InvariantCulture));
                accounts.Add(tempId, acc);
            }
            return line;
        }

        public void SaveData()
        {
            string date = DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            using (var writer = new StreamWriter(date))
            {
                writer.WriteLine(CustomerCount.ToString());
                
                string line = "";
                foreach (var item in customers)
                {
                    line = string.Join(";", new string[]
                    {
                        item.Value.Id.ToString(),
                        item.Value.OrganizationNumber,
                        item.Value.OrganizationName,
                        item.Value.OrganizationAddress,
                        item.Value.OrganizationCity,
                        item.Value.OrganizationRegion,
                        item.Value.OrganizationZipCode,
                        item.Value.OrganizationCountry,
                        item.Value.OrganizationPhoneNumber
                    });
                    writer.WriteLine(line);
                }

                writer.WriteLine(AccountCount.ToString());
                foreach (var item in accounts)
                {
                    line = string.Join(";", new string[]
                    {
                        item.Value.AccountNumber.ToString(),
                        item.Value.CustomerId.ToString(),
                        item.Value.Balance.ToString()
                    });
                    writer.WriteLine(line);
                }
            }
            SaveTransactions();
        }

        private void SaveTransactions()
        {
            string fileName = "Transactions-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            using(var writer = new StreamWriter(fileName))
            {
                var transactionCount = (from account in accounts
                                       where account.Value.transactions.Count > 0
                                       select account.Value.transactions.Count).Sum();
                writer.WriteLine(transactionCount.ToString());

                var accountWithHistory = from account in accounts
                                         where account.Value.transactions.Count > 0
                                         select account.Value.transactions;
                string line = "";

                foreach (var item in accountWithHistory)
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        line = String.Join(";", new string[]
                        {
                            item[i].Date,
                            item[i].Sender.ToString(),
                            item[i].Reciever.ToString(),
                            item[i].Amount.ToString(),
                            item[i].CurrentBalance.ToString(),
                            item[i].Type
                        });
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public void AddCustomer()
        {
            int latestCust = (from customer in customers
                              select customer.Value.Id).Max();
            int id = latestCust + 1;
            Console.WriteLine();
            Console.WriteLine("Fields that must be filled are marked with '*'");
            Console.Write("Organization number*: ");
            string orgNum = Console.ReadLine();
            Console.Write("Organization name*: ");
            string orgName = Console.ReadLine();
            Console.Write("Organization address*: ");
            string orgAddress = Console.ReadLine();
            Console.Write("Organization city*: ");
            string orgCity = Console.ReadLine();
            Console.Write("Organization zipcode*: ");
            string orgZip = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(orgNum) && !String.IsNullOrWhiteSpace(orgName) && !String.IsNullOrWhiteSpace(orgAddress) 
                    && !String.IsNullOrWhiteSpace(orgCity) && !String.IsNullOrWhiteSpace(orgZip))
            {
                Console.Write("Organization Region: ");
                string orgReg = Console.ReadLine();
                Console.Write("Organization country: ");
                string orgCountry = Console.ReadLine();
                Console.Write("Organization phonenumber: ");
                string orgPhone = Console.ReadLine();
                Console.WriteLine();
                customers.Add(id, new Customer(id, orgNum, orgName, orgAddress, orgCity, orgReg, orgZip, orgCountry, orgPhone));
                CustomerCount++;
                Console.WriteLine(" **  Customer " + id.ToString() + " added  ** ");
                CreateAccountForNewCust(id);
            }
            else
            {
                Console.WriteLine(" ** Error! Missing input.. ** ");
                Console.WriteLine(" * Customer was not created * ");
            }
        }

        private void CreateAccountForNewCust(int id)
        {
            int latestAccount = (from account in accounts
                                 select account.Value.AccountNumber).Max();
            accounts.Add(latestAccount + 1, new Account(latestAccount + 1, id, 0));
            AccountCount++;
            Console.WriteLine();
            Console.WriteLine(" ** Account " + (latestAccount + 1).ToString() + " added to customer " + id + ". **");
        }

        public void RemoveCustFromBank()
        {
            Console.WriteLine(" * Remove customer from bank. *");
            Console.Write(" * Customerid: ");
            string id = Console.ReadLine();
            if (int.TryParse(id, out int custId))
            {
                RemoveCustomer(custId);
            }
            else
            {
                Console.WriteLine(" * Customer not found. * ");
            }
        }

        public void RemoveCustomer(int id)
        {
            var keys = customers.Keys;
            var getBalance = (from account in accounts
                               where account.Value.CustomerId == id
                               select account.Value.Balance).Sum();
            if (keys.Contains(id))
            {
                if (getBalance == 0)
                {
                    customers.Remove(id);
                    CustomerCount--;
                    Console.WriteLine();
                    Console.WriteLine(" ** Customer " + id.ToString() + " removed. ** ");
                }
                else
                {
                    Console.WriteLine(" * Customer still has an account with balance left on it. * ");
                }
            }
            else
            {
                Console.WriteLine("Could not find customer.");
            }
        }

        public void AddAccount()
        {
            Console.Write(" * Customer ID: ");
            string cust = Console.ReadLine();
            if (int.TryParse(cust, out int custID))
            {
                var findCust = (from customer in customers
                               where customer.Value.Id == custID
                               select customer).Count();
                if(findCust == 1)
                {
                    int latestAccount = (from account in accounts
                                        select account.Value.AccountNumber).Max();
                    accounts.Add(latestAccount + 1, new Account(latestAccount + 1, custID, 0));
                    AccountCount++;
                    Console.WriteLine();
                    Console.WriteLine(" ** Account " + (latestAccount + 1).ToString() + " added to customer " + cust + ". **");
                }
            }
            else
            {
                Console.WriteLine("Could not find that customer.");
            }
        }

        public void RemoveAccFromBank()
        {
            Console.WriteLine(" * Remove account from bank. * ");
            Console.Write(" * Customer ID: ");
            string id = Console.ReadLine();
            if (int.TryParse(id, out int custID))
            {
                int findCust = (from customer in customers
                                where customer.Value.Id == custID
                                select customer).Count();
                if (findCust == 1)
                {
                    Console.Write(" * Account ID: ");
                    string acc = Console.ReadLine();
                    if (int.TryParse(acc, out int accID))
                    {
                        var selAccount = (from account in accounts
                                          where account.Value.AccountNumber == accID
                                          select account).Single();
                        RemoveAccount(selAccount.Value.AccountNumber);
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

        public void RemoveAccount(int id)
        {
            var keys = accounts.Keys;
            
            if (keys.Contains(id) && accounts[id].Balance == 0)
            {
                accounts.Remove(id);
                AccountCount--;
                Console.WriteLine();
                Console.WriteLine(" ** Account removed from customer " + id.ToString() + ". ** ");
            }
            else
            {
                Console.WriteLine("Account still contains currency.");
            }
        }

        public void ShowCustomerImage()
        {
            Console.WriteLine(" * Show customerimage. * ");
            Console.Write(" * Customer ID or account ID: ");
            string custOrAcc = Console.ReadLine();
            Console.WriteLine();
            if (int.TryParse(custOrAcc, out int custOrAccID))
            {
                if (customers.ContainsKey(custOrAccID))
                {
                    var foundCust = (from customer in customers
                                     where custOrAccID == customer.Value.Id
                                     select customer.Value).Single();
                    var findAccounts = from account in accounts
                                       where account.Value.CustomerId == custOrAccID
                                       select account;
                    PrintCustomerImage(findAccounts, foundCust);
                }
                else if (accounts.ContainsKey(custOrAccID))
                {
                    var acc = accounts[custOrAccID];
                    var findAccounts = from account in accounts
                                       where account.Value.CustomerId == acc.CustomerId
                                       select account;
                    var foundCust = (from customer in customers
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

        private void PrintCustomerImage(IEnumerable<KeyValuePair<int, Account>> findAccounts, Customer foundCust)
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

        public void SearchCustomer()
        {
            Console.WriteLine(" * Search customer. *");
            Console.Write(" * Name or zipcode: ");
            string cust = Console.ReadLine();
            var findCust = from customer in customers
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

        public void GetAccountImage()
        {
            Console.WriteLine(" * Accountimage * ");
            Console.Write(" * Account ID: ");
            string acc = Console.ReadLine();
            if (int.TryParse(acc, out int accID))
            {
                if (accounts.ContainsKey(accID))
                {
                    var findAcc = (from account in accounts
                                   where accID == account.Value.AccountNumber
                                   select account.Value).Single();
                    FindTransactions(findAcc);
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

        private void FindTransactions(Account findAcc)
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

        private void CheckAndPrintTypeOfTransaction(Transaction item)
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
    }
}
