using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Menu : Bank
    {
        public Database DataBaseFromBank;

        public Menu(Database dataBase)
        {
            DataBaseFromBank = dataBase;
        }

        public void PrintMenu()
        {
            Console.WriteLine("                                             ");
            Console.WriteLine(" ************************************************* ");
            Console.WriteLine("     $$$//$$$$$$$$$$$$$$//$$$$$$$$$$$$$$$//$   ");
            Console.WriteLine("     $$//WELCOME TO THE//BANK OF GABRIEL//$$   ");
            Console.WriteLine("     $//$$$$$$$$$$$$$$//$$$$$$$$$$$$$$$//$$$   ");
            Console.WriteLine(" ************************************************* ");
            Console.WriteLine("                                             ");
            Console.WriteLine(" ** Getting customer data...         ** ");
            DataBase.GetDataBase();
            Console.WriteLine(" ** We currently have: " + DataBase.CustomerCount + " customers. **");
            Console.WriteLine(" ** We currently have: " + DataBase.AccountCount + " accounts. **");
            var totalBalance = (from account in DataBase.accounts.Values
                                select account.Balance).Sum();
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
            Console.WriteLine("   |[10] Show accountimage.                   |   ");
            Console.WriteLine("   |[11] Change interest.                     |   ");
            Console.WriteLine("   |[12] Calculate interest.                  |   ");
            Console.WriteLine("   |[13] Change credit.                       |   ");
            Console.WriteLine("   |[14] Change debtinterest.                 |   ");
            Console.WriteLine("   |__________________________________________|   ");
            Console.WriteLine(" *************************************************");
        }

        public void CheckInput()
        {
            string userInput = Console.ReadLine();
            if (userInput.Length >= 1)
            {

                if (userInput.Equals("0"))
                {
                    //Save the data and exit the application. Bank / DB / FM
                    SaveAndExit();
                }
                if (userInput.Equals("1"))
                {
                    //Search customer. DB
                    DataBase.SearchCustomer();
                }
                if (userInput.Equals("2"))
                {
                    //Show full customer info. DB
                    DataBase.ShowCustomerImage();
                }
                if (userInput.Equals("3"))
                {
                    //Create new customer. DB
                    Console.WriteLine(" * Create new customer. *");
                    DataBase.AddCustomer();
                }
                if (userInput.Equals("4"))
                {
                    //Remove customer from bank. DB
                    DataBase.RemoveCustFromBank();
                }
                if (userInput.Equals("5"))
                {
                    //Create new account. DB
                    Console.WriteLine(" * Create new account. * ");
                    DataBase.AddAccount();
                }
                if (userInput.Equals("6"))
                {
                    //Remove account from bank DB
                    DataBase.RemoveAccFromBank();
                }
                if (userInput.Equals("7"))
                {
                    //Deposit. Bank
                    DepositFromAccount();
                }
                if (userInput.Equals("8"))
                {
                    //Withdraw. Bank
                    WithdrawFromAccount();
                }
                if (userInput.Equals("9"))
                {
                    //Transfer. Bank
                    Transfer();
                }
                if (userInput.Equals("10"))
                {
                    //Get the image for that account. DB
                    DataBase.GetAccountImage();
                }
                if (userInput.Equals("11"))
                {
                    //Set the interest for an account. Bank
                    SetInterestForAccount();
                }
                if (userInput.Equals("12"))
                {
                    //Calculate and add interest to accounts. Bank
                    CalculateAndAddInterestToAccounts();
                }
                if (userInput.Equals("13"))
                {
                    //Set credit for the account. Bank
                    SetCreditForAccount();
                }
                if (userInput.Equals("14"))
                {
                    //Set the debtinterest for the account. Bank
                    SetDebtInterestForAccount();
                }
            }
        }
    }
}
