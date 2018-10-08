using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace BankApp
{
    class FileManager : Database
    {
        public int Customers;
        public int Accounts;
        public Dictionary<int, Customer> CustomersFromFile;
        public Dictionary<int, Account> AccountsFromFile;

        public FileManager(Dictionary<int, Customer> customers, Dictionary<int, Account> accounts, int customerCount, int accountCount)
        {
            Customers = customerCount;
            Accounts = accountCount;
            CustomersFromFile = customers;
            AccountsFromFile = accounts;
        }

        public void GetData()
        {
            using (var data = new StreamReader("bankdata.txt"))
            {
                string[] line = data.ReadLine().Split(';');
                if (int.TryParse(line[0], out int numOfCust))
                {
                    Customers = numOfCust;
                    line = GetCustomers(data, line, numOfCust);
                }
                else
                {
                    Console.WriteLine("Error...");
                }
                line = data.ReadLine().Split(';');
                if (int.TryParse(line[0], out int numOfAcc))
                {
                    Accounts = numOfAcc;
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
                CustomersFromFile.Add(tempId, cust);
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
                AccountsFromFile.Add(tempId, acc);
            }
            return line;
        }

        public void SaveData()
        {
            string date = DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            using (var writer = new StreamWriter(date))
            {
                writer.WriteLine(Customers.ToString());
                string line = "";
                foreach (var item in CustomersFromFile)
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
                writer.WriteLine(Accounts.ToString());
                foreach (var item in AccountsFromFile)
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
            using (var writer = new StreamWriter(fileName))
            {
                var transactionCount = (from account in AccountsFromFile
                                        where account.Value.transactions.Count > 0
                                        select account.Value.transactions.Count).Sum();
                writer.WriteLine(transactionCount.ToString());

                var accountWithHistory = from account in AccountsFromFile
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

        public static void SaveTransaction(Transaction transaction)
        {
            using (var writer = new StreamWriter("TransactionLog.txt", true))
            {
                string line = string.Join(";", new string[]
                {
                    transaction.Date,
                    transaction.Sender.ToString(),
                    transaction.Reciever.ToString(),
                    transaction.Amount.ToString(),
                    transaction.CurrentBalance.ToString(),
                    transaction.Type
                });
                writer.WriteLine(line);
            }
        }
    }
}
