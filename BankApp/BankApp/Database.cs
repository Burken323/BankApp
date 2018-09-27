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
                writer.WriteLine(numberOfCust);
                
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
                    Console.WriteLine(line);
                    writer.WriteLine(line);
                }

                writer.WriteLine(numberOfAcc);
                foreach (var item in accounts)
                {
                    line = string.Join(";", new string[]
                    {
                        item.Value.accountNumber.ToString(),
                        item.Value.customerId.ToString(),
                        item.Value.balance.ToString()
                    });
                    Console.WriteLine(line);
                    writer.WriteLine(line);

                }
                
            }
             
            
        }

        public void AddCustomer()
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer()
        {
            throw new NotImplementedException();
        }

        public void AddAccount()
        {
            throw new NotImplementedException();
        }

        public void RemoveAccount()
        {
            throw new NotImplementedException();
        }
    }
}
