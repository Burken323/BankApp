using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Database
    {
        public int numberOfCust { get; set; }
        public int numberOfAcc { get; set; }
        //currentEditDate
        //latestEditDate
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
                    numberOfCust = numOfCust;
                    line = GetCustomers(data, line, numOfCust);
                }
                else
                {
                    Console.WriteLine("Error...");
                }

                line = data.ReadLine().Split(';');

                if (int.TryParse(line[0], out int numOfAcc))
                {
                    numberOfAcc = numOfAcc;
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

            using (var writer = new StreamWriter(@"C:\Users\gabbe\source\repos\BankApp\BankApp\BankApp\saved-bankdata.txt"))
            {
                writer.WriteLine(numberOfCust.ToString());
                
                string line = "";
                foreach (var item in customers)
                {
                    line = string.Join(";", new string[]
                    {
                        item.Value.id.ToString(),
                        item.Value.organizationNumber,
                        item.Value.organizationName,
                        item.Value.orgAddress,
                        item.Value.orgCity,
                        item.Value.orgRegion,
                        item.Value.orgZipCode,
                        item.Value.orgCountry,
                        item.Value.orgPhoneNumber
                    });
                    writer.WriteLine(line);
                }

                writer.WriteLine(numberOfAcc.ToString());
                foreach (var item in accounts)
                {
                    line = string.Join(";", new string[]
                    {
                        item.Value.accountNumber.ToString(),
                        item.Value.customerId.ToString(),
                        item.Value.balance.ToString()
                    });
                    writer.WriteLine(line);

                }
            }
        }

        public void AddCustomer()
        {
            int latestCust = (from customer in customers
                              select customer.Value.id).Max();
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
            if (orgNum.Length > 0 && orgName.Length > 0 && orgAddress.Length > 0 && orgCity.Length > 0 && orgZip.Length > 0)
            {
                Console.Write("Organization Region: ");
                string orgReg = Console.ReadLine();
                Console.Write("Organization country: ");
                string orgCountry = Console.ReadLine();
                Console.Write("Organization phonenumber: ");
                string orgPhone = Console.ReadLine();
                Console.WriteLine();
                customers.Add(id, new Customer(id, orgNum, orgName, orgAddress, orgCity, orgReg, orgZip, orgCountry, orgPhone));
                numberOfCust++;
                Console.WriteLine(" **  Customer " + id.ToString() + " added  ** ");
                int latestAccount = (from account in accounts
                                     select account.Value.accountNumber).Max();
                accounts.Add(latestAccount + 1, new Account(latestAccount + 1, id, 0));
                numberOfAcc++;
                Console.WriteLine();
                Console.WriteLine(" ** Account " + (latestAccount + 1).ToString() + " added to customer " + id + ". **");
            }
            else
            {
                Console.WriteLine(" ** Error! Missing input.. ** ");
                Console.WriteLine(" * Customer was not created * ");
            }

        }

        public void RemoveCustomer(int id)
        {
            var keys = customers.Keys;
            if (keys.Contains(id))
            {
                customers.Remove(id);
                numberOfCust--;
                Console.WriteLine();
                Console.WriteLine(" ** Customer " + id.ToString() + " removed. ** ");
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
                               where customer.Value.id == custID
                               select customer).Count();
                if(findCust == 1)
                {
                    int latestAccount = (from account in accounts
                                        select account.Value.accountNumber).Max();
                    accounts.Add(latestAccount + 1, new Account(latestAccount + 1, custID, 0));
                    numberOfAcc++;
                    Console.WriteLine();
                    Console.WriteLine(" ** Account " + (latestAccount + 1).ToString() + " added to customer " + cust + ". **");
                }
            }
            else
            {
                Console.WriteLine("Could not find that customer.");
            }
        }

        public void RemoveAccount(int id)
        {
            var keys = accounts.Keys;
            
            if (keys.Contains(id) && accounts[id].balance == 0)
            {
                accounts.Remove(id);
                numberOfAcc--;
                Console.WriteLine();
                Console.WriteLine(" ** Account removed from customer " + id.ToString() + ". ** ");
            }
            else
            {
                Console.WriteLine("Account still contains currency.");
            }
        }
    }
}
