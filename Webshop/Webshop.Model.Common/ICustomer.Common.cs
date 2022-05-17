using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Mode.Common
{
    public interface ICustomer
    {
        DateTime CreatedAt { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        Guid Id { get; set; }
        string LastName { get; set; }
        DateTime ModifiedAt { get; set; }
        string PasswordCustomer { get; set; }
        string UserName { get; set; }
    }
}
