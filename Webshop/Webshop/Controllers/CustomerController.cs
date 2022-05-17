using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Webshop.Common;
using Webshop.Model;
using Webshop.Models;
using Webshop.Service;
using Webshop.Service.Common;



namespace Webshop.Controllers
{
    
    public class CustomerController : ApiController
    {
        private ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<List<Customer>> GetCustomersAsync([FromUri]Page page, [FromUri] Filter filter, [FromUri]Sort sort)
        {
            List<Customer> customers = await customerService.GetCustomersAsync(page,sort,filter);
            return customers;
        }


        public async Task<HttpResponseMessage> GetCustomerByIdAsync(Guid id)
        {

            var customer = await customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Customer by id: {id} is not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }


        public async Task<HttpResponseMessage> PostCustomerAsync(CustomerRest customerRest)
        {
            if (customerRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, customerRest);
            }
            else
            {
                Customer customer = new Customer();
                customer.Id = Guid.NewGuid();
                customer.FirstName = customerRest.FirstName;
                customer.LastName = customerRest.LastName;
                customer.UserName = customerRest.UserName;
                customer.PasswordCustomer = customerRest.PasswordCustomer;
                customer.Email = customerRest.Email;
                customer.CreatedAt = DateTime.Today;
                customer.ModifiedAt = customer.CreatedAt;


                CustomerDetails customerDetails = new CustomerDetails();

                customerDetails.CustomerId= customer.Id;
                customerDetails.Address1 = customerRest.Address1;
                customerDetails.Address2 = customerRest.Address2;
                customerDetails.City = customerRest.City;
                customerDetails.PostalCode = customerRest.PostalCode;
                customerDetails.Country = customerRest.Country;
                customerDetails.Mobile = customerRest.Mobile;

                customer.CustomerDetails = customerDetails;

                await customerService.PostNewCustomerAsync(customer);
                return Request.CreateResponse(HttpStatusCode.OK, "Added to the Customer's base");
            }

        }

        public async Task<HttpResponseMessage> PutAsync(Guid id,[FromBody] CustomerRest customerRest)
        {

            if (customerRest==null)

            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Must enter customer's first name,last name and the username");
            }
            else
            {
                Customer customer = new Customer();
                customer.FirstName = customerRest.FirstName;
                customer.LastName = customerRest.LastName;
                customer.UserName = customerRest.UserName;
                customer.ModifiedAt = DateTime.Today;
                await customerService.PutAsync(id, customer);
                return Request.CreateResponse(HttpStatusCode.OK, $"Customer of id = {id} has changed first name to {customer.FirstName}, last name to {customer.LastName} and username to {customer.UserName}");

            }
        }

        public async Task<HttpResponseMessage> DeleteCustomerAsync(Guid id)
        {
            //var x = await customerService.GetCustomerByIdAsync(id);
            if (id == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Customer by id: {id} is not found");
            }
            else
            {
                await customerService.DeleteCustomerAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, $"Customer of id= {id} is deleted");
            }
        }

    }
}