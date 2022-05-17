using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;

namespace Webshop.Repository
{
    
    public class ProductCategoryRepository
    {
        static string connectionString = @"Data Source=DESKTOP-4HPED1P;Initial Catalog = WebshopdB;Integrated Security = True";

        public async Task<List<ProductCategoryModel>> GetCategoryAsync(Page page, Sort sort, Filter filter)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            using (connection)
            {

                StringBuilder query = new StringBuilder();
                query.Append( "SELECT * FROM ProductCategory ");

                if (filter != null)
                {
                    query.Append(" WHERE 1=1");

                    if (!string.IsNullOrWhiteSpace(filter.ProductCategoryName))
                    {
                        query.Append($" AND ProductCategoryName LIKE '%{filter.ProductCategoryName}'");
                    }

                    if (!string.IsNullOrWhiteSpace(filter.ProductCategoryDescription))
                    {
                        query.Append($" AND ProductCategoryDescription LIKE '%{filter.ProductCategoryDescription}'");
                    }

                }

                if (sort != null)
                {
                    if (!string.IsNullOrWhiteSpace(sort.OrderBy))
                    {
                        query.Append($" ORDER BY {sort.OrderBy}");
                    }
                }

                if (page != null)
                {
                    query.Append($" OFFSET ({page.PageNumber} -1)  *  {page.RecordsPerPage}  ROWS  FETCH  NEXT  {page.RecordsPerPage}  ROWS  ONLY ");
                }

                query.Append(";");


                SqlCommand command = new SqlCommand(query.ToString(), connection);
                SqlDataReader reader = command.ExecuteReader();
                List<ProductCategoryModel> result = new List<ProductCategoryModel>();

                if (reader.HasRows)
                {
                    
                    while (await reader.ReadAsync())
                    {
                        var category = new ProductCategoryModel();
                        category.CategoryId = reader.GetGuid(0);
                        category.CategoryName = reader.GetString(1);
                        category.CategoryDescription = reader.GetString(2);

                        result.Add(category);



                    }
                    connection.Close();
                    reader.Close();
                    return result;
                }
                else
                {
                    return null;
                }

            }

        }


        public async Task<ProductCategoryModel> GetCategoryByIdAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM ProductCategory WHERE ID = '{id}';", connection);
                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    ProductCategoryModel category = new ProductCategoryModel
                    {
                        CategoryId = reader.GetGuid(0),
                        CategoryName = reader.GetString(1),
                        CategoryDescription = reader.GetString(2),
                        CreatedAt = reader.GetDateTime(3),
                        ModifiedAt = reader.GetDateTime(4)
                    };
                    reader.Close();
                    return category;

                }
                else
                {
                    reader.Close();
                    return null;
                }


            }
        }



        public async Task PostCategoryAsync(ProductCategoryModel category)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string newCategory = $"INSERT into ProductCategory(Id, CategoryName, CategoryDescription, CreatedAt, ModifiedAt) VALUES" +
                    $"('{category.CategoryId}'," +
                    $"'{category.CategoryName}'," +
                    $"'{category.CategoryDescription}'," +
                    $"'{category.CreatedAt}'," +
                    $"'{category.ModifiedAt}')";

            using (connection)
            {
                SqlCommand command = new SqlCommand(newCategory, connection);
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(newCategory, connection);

                await command.ExecuteNonQueryAsync();
                connection.Close();
            }

        }

        public async Task UpdateCategoryAsync(Guid id, ProductCategoryModel category)
        { 
            if (await this.GetCategoryByIdAsync(id) == null)
            {
                return;
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string updateCategory = $"UPDATE ProductCategory SET CategoryName = '{category.CategoryName}',Id = '{category.CategoryId}'," +
                    $"CategoryDescription = '{category.CategoryDescription}'," +
                    $"CreatedAt = '{category.CreatedAt}', ModifiedAt = '{category.ModifiedAt}'";

                using (connection)
                {
                    SqlCommand command = new SqlCommand(updateCategory, connection);
                    await connection.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(updateCategory, connection);

                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }

            }
        }








        public async Task DeleteByIdAsync(Guid Id)
        {
            if (await this.GetCategoryByIdAsync(Id) == null)
            {
                return;
            }

            SqlConnection connection = new SqlConnection(connectionString);
            string deleteCategory = $"DELETE FROM ProductCategory WHERE ID = '{Id}';";

            using (connection)
            {
                SqlCommand command = new SqlCommand(deleteCategory, connection);
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(deleteCategory, connection);

                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
    }
}
