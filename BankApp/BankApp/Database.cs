﻿using System;
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
            Console.Write("Organization number: ");
            string orgNum = Console.ReadLine();
            Console.Write("Organization name: ");
            string orgName = Console.ReadLine();
            Console.Write("Organization address: ");
            string orgAddress = Console.ReadLine();
            Console.Write("Organization city: ");
            string orgCity = Console.ReadLine();
            Console.Write("Organization Region: ");
            string orgReg = Console.ReadLine();
            Console.Write("Organization zipcode: ");
            string orgZip = Console.ReadLine();
            Console.Write("Organization country: ");
            string orgCountry = Console.ReadLine();
            Console.Write("Organization phonenumber: ");
            string orgPhone = Console.ReadLine();
            Console.WriteLine();
            customers.Add(id, new Customer(id, orgNum, orgName, orgAddress, orgCity, orgReg, orgZip, orgCountry, orgPhone));
            numberOfCust++;
            Console.WriteLine(" **  Customer added  ** ");
            
        }

        public void RemoveCustomer(int id)
        {
            var keys = customers.Keys;
            if (keys.Contains(id))
            {
                customers.Remove(id);
            }
            else
            {
                Console.WriteLine("Could not find customer.");
            }
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
