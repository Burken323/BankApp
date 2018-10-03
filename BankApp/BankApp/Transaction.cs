using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Transaction
    {
        public string Date { get; set; }
        public int Sender { get; set; }
        public int Reciever { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentBalance { get; set; }
        public string Type { get; set; }

        public Transaction(string dateOfTransfer, int senderID, int recieverID, decimal currency, decimal currBalance, string typeOfTransfer)
        {
            Date = dateOfTransfer;
            Sender = senderID;
            Reciever = recieverID;
            Amount = currency;
            CurrentBalance = currBalance;
            Type = typeOfTransfer;
        }
    }
}
