using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class Customer
    {
        public int Id { get; set; }
        public string OrganizationNumber { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string OrganizationCity { get; set; }
        public string OrganizationRegion { get; set; }
        public string OrganizationZipCode { get; set; }
        public string OrganizationCountry { get; set; }
        public string OrganizationPhoneNumber { get; set; }
        

        public Customer()
        {

        }
        
        public Customer( int id, string orgNumber, string orgName, string orgAddress, string orgCity, string orgRegion, 
                            string orgZipcode, string orgCountry, string orgPhoneNum)
        {
            Id = id;
            OrganizationNumber = orgNumber;
            OrganizationName = orgName;
            OrganizationAddress = orgAddress;
            OrganizationCity = orgCity;
            OrganizationRegion = orgRegion;
            OrganizationZipCode = orgZipcode;
            OrganizationCountry = orgCountry;
            OrganizationPhoneNumber = orgPhoneNum;

        }

        
    }
}
