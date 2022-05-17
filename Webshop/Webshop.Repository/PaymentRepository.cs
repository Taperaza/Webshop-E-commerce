using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model.Common;
using Webshop.Model;
using Webshop.Repository.Common;

namespace Webshop.Repository
{
    public class PaymentRepository : IPaymentRepositoryCommon
    {
        static string connectionString = @"Data Source=DESKTOP-V8JBKRE;Initial Catalog=SQLTest;Integrated Security=True";

        SqlConnection connection = new SqlConnection(connectionString);


        //GET

        public async Task<List<PaymentModel>> GetAllAsync(Paging paging, PaymentFiltering filtering, Sorting sorting)
        {
            await connection.OpenAsync();
            using (connection)
            {

                StringBuilder commandString = new StringBuilder();
                commandString.Append("SELECT * FROM Payment INNER JOIN Customer ON Payment.CustomerId=Customer.Id");

                if (filtering != null)
                {
                    commandString.Append("WHERE 1=1");
                    if (!string.IsNullOrEmpty(filtering.Customer))
                    {
                        commandString.Append("AND Customer LIKE '" + filtering.Customer + "%'");
                    }
                    if (!string.IsNullOrEmpty(filtering.PaymentType))
                    {
                        commandString.Append("AND PaymentType LIKE '" + filtering.PaymentType + "%'");
                    }
                    if (!string.IsNullOrEmpty(filtering.ProviderName))
                    {
                        commandString.Append("AND ProviderName LIKE '" + filtering.ProviderName + "%'");
                    }

                }

                if (sorting != null)
                {
                    commandString.Append($"ORDER BY '{sorting.SortBy}' ");
                }

                if (paging != null)
                {
                    commandString.Append($"OFFSET({paging.PageNumber} - 1) * {paging.RecordsPerPage} ROWS FETCH NEXT {paging.RecordsPerPage} ROWS ONLY ");
                }
                commandString.Append(";");


                SqlCommand command = new SqlCommand(commandString.ToString(), connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                List<PaymentModel> listofPayment = new List<PaymentModel>();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var pay = new PaymentModel();

                        pay.Id = reader.GetGuid(0);
                        pay.CustomerId = reader.GetGuid(1);
                        pay.PaymentType = reader.GetString(2);
                        pay.ProviderName = reader.GetString(3);
                        pay.AccountNo = reader.GetInt32(4);
                        pay.CreatedAt = reader.GetDateTime(5);
                        pay.ModifiedAt = reader.GetDateTime(6);

                        listofPayment.Add(pay);
                    }

                    connection.Close();
                    reader.Close();
                }
                return listofPayment;
            }



        }


        //GET by ID

        public async Task<PaymentModel> GetIdAsync(Guid Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"SELECT * FROM Payment WHERE Id = '{Id}';", connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            PaymentModel paymentModel = new PaymentModel();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    paymentModel.Id = reader.GetGuid(0);
                    paymentModel.CustomerId = reader.GetGuid(1);
                    paymentModel.PaymentType = reader.GetString(2);
                    paymentModel.ProviderName = reader.GetString(3);
                    paymentModel.AccountNo = reader.GetInt32(4);
                    paymentModel.CreatedAt = reader.GetDateTime(5);
                    paymentModel.ModifiedAt = reader.GetDateTime(6);
                }
                reader.Close();

            }
            connection.Close();
            return paymentModel;
        }


        //POST

        public async Task PostAsync(PaymentModel pay)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string editColumn = $"INSERT INTO Payment(Id, Customer, PaymentType, ProviderName, AccountNo, CreatedAt, ModifiedAt) VALUES" +
                    $"('{pay.Id}'," +
                    $"'{pay.CustomerId}'," +
                    $"'{pay.PaymentType}'," +
                    $"'{pay.ProviderName}'," +
                    $"'{pay.AccountNo}'," +
                    $"'{pay.CreatedAt}'," +
                    $"'{pay.ModifiedAt}')";

            using (connection)
            {
                SqlCommand command = new SqlCommand(editColumn, connection);
                await connection.OpenAsync();

                SqlDataAdapter adapter = new SqlDataAdapter(editColumn, connection);


                await command.ExecuteNonQueryAsync();

                connection.Close();
            }

        }


        //UPDATE

        public async Task PutAsync(Guid Id, PaymentModel pay)
        {
            if (await this.GetIdAsync(Id) == null)
            {
                return;
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string payEdit = $"UPDATE Payment SET Id = '{pay.Id}', Customer ='{pay.CustomerId}" +
                    $"PaymentType = '{pay.PaymentType}', ProviderName = '{pay.ProviderName}', AccountNo = '{pay.AccountNo}'," +
                    $"CreatedAt = '{pay.CreatedAt}', ModifiedAt = '{pay.ModifiedAt}'";

                using (connection)
                {
                    SqlCommand command = new SqlCommand(payEdit, connection);
                    await connection.OpenAsync();

                    SqlDataAdapter adapter = new SqlDataAdapter(payEdit, connection);
                    await command.ExecuteNonQueryAsync();

                    connection.Close();
                }
            }



        }


        //DELETE

        public async Task DeleteIdAsync(Guid Id)
        {
            if (await this.GetIdAsync(Id) == null)
            {
                return;
            }

            SqlConnection connection = new SqlConnection(connectionString);
            string deleteId = $"SELECT * FROM Payment WHERE Id = '{Id}';";

            using (connection)
            {
                SqlCommand command = new SqlCommand(deleteId, connection);
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(deleteId, connection);


                await command.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
    }
}
