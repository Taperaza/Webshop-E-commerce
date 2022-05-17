using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Mode.Common
{
    public interface ICustomerDetails
    {
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string Country { get; set; }
        Guid CustomerId { get; set; }
        string Mobile { get; set; }
        int PostalCode { get; set; }
    }
}
