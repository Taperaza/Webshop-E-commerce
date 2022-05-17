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
    public class DiscountRepository : IDiscountRepositoryCommon
    {
        static string connectionString = @"Data Source=DESKTOP-V8JBKRE;Initial Catalog=SQLTest;Integrated Security=True";



        //GET
        public async Task<List<DiscountModel>> GetAllAsync(Paging paging, Sorting sorting)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            using (connection)
            {

                StringBuilder commandString = new StringBuilder();
                commandString.Append(" SELECT  *  FROM  Discount ");

                if (sorting != null)
                {
                    if (!string.IsNullOrWhiteSpace(sorting.SortBy))
                    {
                        commandString.Append($" ORDER  BY  {sorting.SortBy}  ");
                    }

                }

                if (paging != null)
                {
                    commandString.Append($"  OFFSET  (  {paging.PageNumber}  -  1  )  *  {paging.RecordsPerPage}  ROWS  FETCH  NEXT  {paging.RecordsPerPage}  ROWS  ONLY");
                }
                commandString.Append(";");

                SqlCommand command = new SqlCommand(commandString.ToString(), connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                List<DiscountModel> listofDiscounts = new List<DiscountModel>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var discount = new DiscountModel();

                        discount.Id = reader.GetGuid(0);
                        discount.DiscountName = reader.GetString(1);
                        discount.Discount = reader.GetInt32(2);
                        discount.Active = reader.GetBoolean(3);
                        discount.CreatedAt = reader.GetDateTime(4);
                        discount.ModifiedAt = reader.GetDateTime(5);

                        listofDiscounts.Add(discount);
                    }

                    reader.Close();
                }
                connection.Close();
                return listofDiscounts;
            }
        }


        //GET by ID

        public async Task<DiscountModel> GetIdAsync(Guid Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"SELECT * FROM Discount WHERE Id = '{Id}';", connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            DiscountModel discountModel = new DiscountModel();


            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    discountModel.Id = reader.GetGuid(0);
                    discountModel.DiscountName = reader.GetString(1);
                    discountModel.Discount = reader.GetInt32(2);
                    discountModel.Active = reader.GetBoolean(3);
                    discountModel.CreatedAt = reader.GetDateTime(4);
                    discountModel.ModifiedAt = reader.GetDateTime(5);
                }
                reader.Close();


            }
            connection.Close();
            return discountModel;
        }


        //POST

        public async Task PostAsync(DiscountModel disc)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string editColumn = $"INSERT INTO Discount(Id, DiscountName, Discount, Active, CreatedAt, ModifiedAt) VALUES" +
                    $"('{disc.Id}'," +
                    $"'{disc.DiscountName}'," +
                    $"'{disc.Discount}'," +
                    $"'{disc.Active}'," +
                    $"'{disc.CreatedAt}'," +
                    $"'{disc.ModifiedAt}')";

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

        public async Task PutAsync(Guid Id, DiscountModel disc)
        {
            if (await this.GetIdAsync(Id) == null)
            {
                return;
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string discEdit = $"UPDATE Discount SET Id = '{disc.Id}'," +
                    $"DiscountName = '{disc.DiscountName}', Discount = '{disc.Discount}', Active = '{disc.Active}'," +
                    $"CreatedAt = '{disc.CreatedAt}', ModifiedAt = '{disc.ModifiedAt}'";

                using (connection)
                {

                    SqlCommand command = new SqlCommand(discEdit, connection);
                    await connection.OpenAsync();

                    SqlDataAdapter adapter = new SqlDataAdapter(discEdit, connection);
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
            string deleteId = $"SELECT * FROM Discount WHERE Id = '{Id}';";

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
