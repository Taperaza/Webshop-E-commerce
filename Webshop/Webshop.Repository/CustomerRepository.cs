using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Webshop.Common;
using Webshop.Model;
using Webshop.Repository.Common;

namespace Webshop.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        //creates and opens connection
        static string connectionString = @"Data Source=DESKTOP-4HPED1P;Initial Catalog=WebshopDB;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public async Task<List<Customer>> GetCustomersAsync(Page page, Sort sort, Filter filter)
        {
            await connection.OpenAsync();
            using (connection)
            {

                StringBuilder query = new StringBuilder();

                query.Append($"SELECT * FROM Customer LEFT JOIN CustomerDetails ON Customer.Id=CustomerDetails.CustomerId");

               // if (filter != null)
               // {
               //     query.Append(" WHERE 1=1");

               //     if (!string.IsNullOrWhiteSpace(filter.FirstName))
               //     {
               //         query.Append($" AND FirstName LIKE '%{filter.FirstName}'");
               //     }

               //     if (!string.IsNullOrWhiteSpace(filter.LastName))
               //     {
               //         query.Append($" AND LastName LIKE '%{filter.LastName}'");
               //     }

               //     if (!string.IsNullOrWhiteSpace(filter.UserName))
               //     {
               //         query.Append($" AND UserName LIKE '%{filter.UserName}'");
               //     }
               // }

                
                
                    
                 
               //query.Append($"ORDER BY FirstName");

               //         //if (!string.IsNullOrWhiteSpace(sort.OrderAD))
               //         //{
               //         //    query.Append($"{sort.OrderBy}");
               //         //}
                    
                


               // if (page != null)
               // {
               //     query.Append($" OFFSET ({page.PageNumber} - 1) * {page.RecordsPerPage} ROWS FETCH NEXT {page.RecordsPerPage} ROWS ONLY");

               // }

                query.Append(";");

                SqlCommand command = new SqlCommand(query.ToString(), connection);

                //executes command
                SqlDataReader dataReader = await command.ExecuteReaderAsync();

                List<Customer> listOfCustomers = new List<Customer>();


                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Id = dataReader.GetGuid(0);
                        customer.FirstName = dataReader.GetString(1);
                        customer.LastName = dataReader.GetString(2);
                        customer.UserName = dataReader.GetString(3);
                        customer.PasswordCustomer = dataReader.GetString(4);
                        customer.Email = dataReader.GetString(5);
                        customer.CreatedAt = dataReader.GetDateTime(6);
                        //customer.ModifiedAt = dataReader.GetDateTime(7);



                        CustomerDetails customerDetails1 = new CustomerDetails();
                        customerDetails1.Address1 = dataReader.GetString(9);
                        customerDetails1.Address2 = dataReader.GetString(10);
                        customerDetails1.City = dataReader.GetString(11);
                        customerDetails1.PostalCode = dataReader.GetInt32(12);
                        customerDetails1.Country = dataReader.GetString(13);
                        customerDetails1.Mobile = dataReader.GetString(14);

                        customerDetails1.CustomerId = customer.Id;
                        customer.CustomerDetails = customerDetails1;

                        listOfCustomers.Add(customer);
                    }
                    dataReader.Close();
                }
                return listOfCustomers;
            }
        }


      
        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand($"SELECT * FROM Customer LEFT JOIN CustomerDetails ON Customer.Id=CustomerDetails.CustomerId WHERE Id='{id}';", connection);


            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            Customer customer = new Customer();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    customer.Id = dataReader.GetGuid(0);
                    customer.FirstName = dataReader.GetString(1);
                    customer.LastName = dataReader.GetString(2);
                    customer.UserName = dataReader.GetString(3);
                    customer.PasswordCustomer = dataReader.GetString(4);
                    customer.Email = dataReader.GetString(5);
                    customer.CreatedAt = dataReader.GetDateTime(6);
                    //customer.ModifiedAt = dataReader.GetDateTime(7);


                    CustomerDetails customerDetails1 = new CustomerDetails();
                    customerDetails1.Address1 = dataReader.GetString(9);
                    customerDetails1.Address2 = dataReader.GetString(10);
                    customerDetails1.City = dataReader.GetString(11);
                    customerDetails1.PostalCode = dataReader.GetInt32(12);
                    customerDetails1.Country = dataReader.GetString(13);
                    customerDetails1.Mobile = dataReader.GetString(14);

                    customerDetails1.CustomerId = customer.Id;
                    customer.CustomerDetails = customerDetails1;

                }
                dataReader.Close();
            }
            connection.Close();
            return customer;
        }

        public async Task PostNewCustomerAsync(Customer customer)
        {
            await connection.OpenAsync();
            string querystring1 = $"INSERT INTO Customer (Id,FirstName,LastName,UserName,PasswordCustomer,Email,CreatedAt) VALUES ('{customer.Id}','{customer.FirstName}','{customer.LastName}','{customer.UserName}','{customer.PasswordCustomer}','{customer.Email}','{customer.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}');";
            string querystring2 = $"INSERT INTO CustomerDetails (CustomerId,Address1,Address2,City,PostalCode,Country,Mobile) VALUES ('{customer.Id}','{customer.CustomerDetails.Address1}','{customer.CustomerDetails.Address2}','{customer.CustomerDetails.City}','{customer.CustomerDetails.PostalCode}','{customer.CustomerDetails.Country}','{customer.CustomerDetails.Mobile}');";

            SqlTransaction transaction;
            transaction = connection.BeginTransaction();
            try
            {
                new SqlCommand(querystring1, connection, transaction)
                   .ExecuteNonQuery();
                new SqlCommand(querystring2, connection, transaction)
                   .ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SqlException sqlError)
            {
                transaction.Rollback();
            }

            connection.Close();
        }



        public async Task PutAsync(Guid id, Customer customer)
        {
            connection.Open();
            string querystring = $"UPDATE Customer SET FirstName= '{customer.FirstName}', LastName ='{customer.LastName}', UserName ='{customer.UserName}', ModifiedAt = '{customer.ModifiedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}' WHERE Id='{id}'";


            SqlDataAdapter dataAdapter = new SqlDataAdapter(querystring, connection);
            DataSet customerData = new DataSet();
            await Task.Run(() => dataAdapter.Fill(customerData, "Customer"));
            connection.Close();
        }


        public async Task DeleteCustomerAsync(Guid id)
        {
            connection.Open();
            string querystring1 = $"DELETE FROM CustomerDetails WHERE CustomerId='{id}'";
            string querystring2 = $"DELETE FROM Customer WHERE Id='{id}'";

            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(querystring1, connection);
            SqlDataAdapter dataAdapter2 = new SqlDataAdapter(querystring2, connection);
            DataSet customerData = new DataSet();
            dataAdapter1.Fill(customerData, "CustomerDetails");
            await Task.Run(() => dataAdapter2.Fill(customerData, "Customer"));
            connection.Close();
        }
    }
}

