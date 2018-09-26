using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Database
    {
        public StreamReader sr;
        public StreamWriter sw;
        public Dictionary<string, Customer> customers;
    }
}
