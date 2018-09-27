using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Transaction
    {
        public string date { get; set; }
        public int sender { get; set; }
        public int reciever { get; set; }
        public decimal amount { get; set; }
        public decimal currentBalance { get; set; }
    }
}
