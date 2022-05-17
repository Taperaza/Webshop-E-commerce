using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop.Model;


namespace Webshop.Models
{
    public class CustomerRest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordCustomer { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        

        //--------CUSTOMER DETAILS

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }

    }

}