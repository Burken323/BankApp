using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Skapa samt starta bankappen.

            Bank bank = new Bank();
            bank.BankOpen();
            
        }
    }
}
