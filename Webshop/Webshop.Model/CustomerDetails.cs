using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Mode.Common;

namespace Webshop.Model
{
    public class CustomerDetails : ICustomerDetails
    {


        public Guid CustomerId { get; set; }


        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }

    }
}
