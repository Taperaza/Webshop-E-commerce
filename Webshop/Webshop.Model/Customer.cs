using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Mode.Common;

namespace Webshop.Model
{
    public class Customer : ICustomer
    {
       

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordCustomer { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public CustomerDetails CustomerDetails { get; set; }


    }
}
