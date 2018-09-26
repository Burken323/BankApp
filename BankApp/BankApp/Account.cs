using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Account
    {
        public int accountNumber { get; set; }
        public int customerNumber { get; set; }
        public Dictionary<int, decimal> balance;

        public Account()
        {

        }
    }
}
