using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class InputManager
    {
        public static bool VerifyCustomer(Database dataBase, string input, out int id)
        {
            if(!int.TryParse(input, out id))
            {
                Console.WriteLine(" * Invalid input. * ");
                return false;
            }
            else if (!dataBase.customers.ContainsKey(id))
            {
                Console.WriteLine(" * Could not find that customer. * ");
                return false;
            }
            return true;
        }

        public static bool VerifyAccount(Dictionary<int, Account> accounts, string input, out int id)
        {
            if (!int.TryParse(input, out id))
            {
                Console.WriteLine(" * Invalid input. * ");
                return false;
            }
            else if (!accounts.ContainsKey(id))
            {
                Console.WriteLine(" * Could not find that account. * ");
                return false;
            }
            return true;
        }

        public static bool VerifyCustomer(Dictionary<int, Customer> customers, string input, out int id)
        {
            if (!int.TryParse(input, out id))
            {
                Console.WriteLine(" * Invalid input. * ");
                return false;
            }
            else if (!customers.ContainsKey(id))
            {
                Console.WriteLine(" * Could not find that customer. * ");
                return false;
            }
            return true;
        }

        public static bool VerifyAccount(Database dataBase, string input, out int id)
        {
            if(!int.TryParse(input, out id))
            {
                Console.WriteLine(" * Invalid input. * ");
                return false;
            }
            else if (!dataBase.accounts.ContainsKey(id))
            {
                Console.WriteLine(" * Could not find that account. * ");
                return false;
            }
            return true;
        }

        public static bool VerifyCurrency(Database dataBase, string input, out decimal id)
        {
            if(!decimal.TryParse(input, out id))
            {
                Console.WriteLine(" * Invalid input. * ");
                return false;
            }
            return true;
        }
    }
}
