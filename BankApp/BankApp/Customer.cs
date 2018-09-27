using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Customer
    {
        public int id { get; set; }
        public string organizationNumber { get; set; }
        public string organizationName { get; set; }
        public string orgAddress { get; set; }
        public string orgCity { get; set; }
        public string orgRegion { get; set; }
        public string orgZipCode { get; set; }
        public string orgCountry { get; set; }
        public string orgPhoneNumber { get; set; }

        public Dictionary<string, Account> accounts;
        public Dictionary<int, Transaction> transactions;

        public Customer()
        {

        }
        
        public Customer( int idNum, string orgNumber, string orgName, string address, string city, string region, string zipcode, string country, string phoneNum)
        {
            id = idNum;
            organizationNumber = orgNumber;
            organizationName = orgName;
            orgAddress = address;
            orgCity = city;
            orgRegion = region;
            orgZipCode = zipcode;
            orgCountry = country;
            orgPhoneNumber = phoneNum;

        }

        
    }
}
