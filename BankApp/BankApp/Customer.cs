using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Customer
    {
        public string customerNumber { get; set; }
        public string organizationName { get; set; }
        public string organizationNumber { get; set; }
        public string zipCode { get; set; }
        public string address { get; set; }
        public Dictionary<string, Account> accounts;
        
        public Customer()
        {
            throw new NotImplementedException();
        }

        
    }
}
