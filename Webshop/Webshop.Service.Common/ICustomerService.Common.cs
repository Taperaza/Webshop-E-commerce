using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;

namespace Webshop.Service.Common
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync(Page page, Sort sort, Filter filter);
        Task<Customer> GetCustomerByIdAsync(Guid id);
        
        Task PostNewCustomerAsync(Customer customer);

        Task PutAsync(Guid id, Customer customer);

        Task DeleteCustomerAsync(Guid id);

    }
}
