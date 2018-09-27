using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Bank
    {
        public Database db;
        public Customer cs;

        public Bank()
        {
            db = new Database();
            cs = new Customer();
        }

        public void BankOpen()
        {
            
            Console.WriteLine("                                             ");
            Console.WriteLine(" ************************************************* ");
            Console.WriteLine("     $$$//$$$$$$$$$$$$$$//$$$$$$$$$$$$$$$//$   ");
            Console.WriteLine("     $$//WELCOME TO THE//BANK OF GABRIEL//$$   ");
            Console.WriteLine("     $//$$$$$$$$$$$$$$//$$$$$$$$$$$$$$$//$$$   ");
            Console.WriteLine(" ************************************************* ");
            Console.WriteLine("                                             ");
            Console.WriteLine(" ** Getting customer data...         ** ");
            db.GetData();
            Console.WriteLine(" ** We currently have: " + db.numberOfCust + " customers. **");
            Console.WriteLine(" ** We currently have: " + db.numberOfAcc + " accounts. **");
            var totalBalance = (from account in db.accounts.Values
                               select account.balance).Sum();
            Console.WriteLine(" ** Total balance: " + totalBalance + ".        **");
            Console.WriteLine(" *************************************************");
            Console.WriteLine("    __________________________________________    ");
            Console.WriteLine("   | Main menu                                |   ");
            Console.WriteLine("   |------------------------------------------|   ");
            Console.WriteLine("   |[0]  Save and exit.                       |   ");
            Console.WriteLine("   |[1]  Search for customer.                 |   ");
            Console.WriteLine("   |[2]  Show customerimage.                  |   ");
            Console.WriteLine("   |[3]  Create customer.                     |   ");
            Console.WriteLine("   |[4]  Remove customer.                     |   ");
            Console.WriteLine("   |[5]  Create account.                      |   ");
            Console.WriteLine("   |[6]  Remove account.                      |   ");
            Console.WriteLine("   |[7]  Deposit.                             |   ");
            Console.WriteLine("   |[8]  Withdraw.                            |   ");
            Console.WriteLine("   |[9]  Transfer.                            |   ");
            Console.WriteLine("   |__________________________________________|   ");
            Console.WriteLine(" *************************************************");
            while (true)
            {
                string userInput = Console.ReadLine();

                int number = int.Parse(userInput);
                
                if (number == 0)
                {
                    Console.WriteLine("  ** Saving data to file.. **  ");
                    db.SaveData();

                    Console.WriteLine("  ** Exiting program. **  ");
                    //Save data into different file and exit.
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                if (userInput[0].Equals("1"))
                {
                    //Search/Choose customer.
                    Console.WriteLine(" * Search customer. *");
                    Console.Write(" * Name or zipcode: ");

                }
                if (userInput[0].Equals("2"))
                {
                    //Show full customer info.
                    Console.WriteLine(" * Show customerimage. * ");
                    Console.Write(" * Customerid: ");


                }
                if (userInput[0].Equals("3"))
                {
                    //Create new customer.
                    Console.WriteLine(" * Create new customer. *");
                    

                }
                if (userInput[0].Equals("4"))
                {
                    //Remove customer from bank.
                    Console.WriteLine(" * Remove customer from bank. *");
                    Console.Write(" * Customerid: ");

                }
                if (userInput[0].Equals("5"))
                {
                    //Create new account.
                    Console.WriteLine(" * Create new account. * ");


                }
                if (userInput[0].Equals("6"))
                {
                    //Remove account from bank
                    Console.WriteLine(" * Remove account from bank. * ");
                    Console.Write(" * Accountid: ");

                }
                if (userInput[0].Equals("7"))
                {
                    //Deposit.
                    Console.WriteLine(" * Deposit.");
                }
                if (userInput[0].Equals("8"))
                {
                    //Withdraw.
                    Console.WriteLine(" * Withdraw. * ");
                }
                if (userInput[0].Equals("9"))
                {
                    //Transfer.
                    Console.WriteLine(" * Transfer. *");
                }
            }
        }

        public void Withdraw()
        {
            throw new NotImplementedException();
        }

        public void Deposit()
        {
            throw new NotImplementedException();
        }

        


    }
}
