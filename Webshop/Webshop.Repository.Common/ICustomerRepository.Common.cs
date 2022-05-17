using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;

namespace Webshop.Repository.Common
{
    public interface ICustomerRepository
    {
        Task DeleteCustomerAsync(Guid id);
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<List<Customer>> GetCustomersAsync(Page page, Sort sort, Filter filter);
        Task PostNewCustomerAsync(Customer customer);

        Task PutAsync(Guid id, Customer customer);

    }
}
