using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;
using Webshop.Repository;
using Webshop.Repository.Common;
using Webshop.Service.Common;

namespace Webshop.Service
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetCustomersAsync(Page page, Sort sort, Filter filter)
        {
            List<Customer> customers = await customerRepository.GetCustomersAsync(page, sort, filter);
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task PostNewCustomerAsync(Customer customer)
        {
            await customerRepository.PostNewCustomerAsync(customer);

        }


        public async Task PutAsync(Guid id, Customer customer)
        {
             await customerRepository.PutAsync(id, customer);

        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            await customerRepository.DeleteCustomerAsync(id);

        }
        
    }
}
