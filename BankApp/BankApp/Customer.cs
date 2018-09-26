using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Customer
    {
        public string Name;
        public string organization;
        public string organizationNumber;
        public string zipCode;
        public string address;

        public Dictionary<string, string> cash;
        
        public Customer()
        {
            throw new NotImplementedException();
        }
    }
}
