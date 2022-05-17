using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;
using Webshop.Repository.Common;


namespace Webshop.Repository
{
    public class ProductRepository : IProductRepository
    { 
        static string connectionString = @"Data Source=DESKTOP-4HPED1P;Initial Catalog = WebshopDB;Integrated Security = True";


        public async Task<List<ProductModel>> GetAllAsync(Page page, Sort sort, Filter filter)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            using (connection)
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT Product.id, Product.ProductName, Product.ProductionDescription, ProductCategory.ProductCategoryName, Product.ProductPrice, Discount.DiscountName, Discount.Discount, Product.ProductCategoryId "
                             + "FROM Product INNER JOIN ProductCategory ON Product.ProductCategoryId = ProductCategory.Id  INNER  JOIN  Discount ON Product.DiscountId = Discount.Id");

                if (filter != null)
                {
                    query.Append(" WHERE 1=1");

                    if (!string.IsNullOrWhiteSpace(filter.ProductName))
                    {
                        query.Append($" AND ProductName LIKE '%{filter.ProductName}%'");
                    }

                    if (!string.IsNullOrWhiteSpace(filter.ProductDescription))
                    {
                        query.Append($" AND ProductDescription LIKE '%{filter.ProductDescription}'");
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
                    query.Append($" OFFSET ({page.PageNumber} -1) * {page.RecordsPerPage} ROWS FETCH NEXT {page.RecordsPerPage} ROWS ONLY");
                }

                query.Append(";");
                

                SqlCommand command = new SqlCommand(query.ToString(), connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                List<ProductModel> result = new List<ProductModel>();
                
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        var product = new ProductModel();
                        var productCategory= new ProductCategoryModel();
                        var discount = new DiscountModel();

                        product.Id = reader.GetGuid(0);
                        product.ProductName = reader.GetString(1);
                        product.ProductDescription = reader.GetString(2);
                        productCategory.CategoryName = reader.GetString(3);
                        product.ProductPrice = reader.GetDecimal(4);
                        discount.Name = reader.GetString(5);
                        discount.Discount = reader.GetDecimal(6);
                        product.ProductCategoryId = reader.GetGuid(7);
                    

                        
                        product.Category = productCategory;
                        product.Discount = discount;
                        

                        result.Add(product);



                    }
                    
                    reader.Close();
                    

                }
                
                connection.Close();
                return result;


            }

        }

 
        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($" SELECT Product.Id, Product.ProductName, Product.ProductionDescription, ProductCategory.ProductCategoryName, Product.ProductPrice, Discount.DiscountName, Discount.Discount, Product.ProductCategoryId " +
                    $" FROM  Product  INNER  JOIN  ProductCategory ON Product.ProductCategoryId = ProductCategory.Id  INNER  JOIN Discount ON Product.DiscountId = Discount.Id  WHERE Product.Id = '{id}' ;", connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            ProductModel product = new ProductModel();

            if (reader.HasRows)
            {

                while (await reader.ReadAsync())
                {
                    
                    var productCategory = new ProductCategoryModel();
                    var discount = new DiscountModel();

                    product.Id = reader.GetGuid(0);
                    product.ProductName = reader.GetString(1);
                    product.ProductDescription = reader.GetString(2);
                    productCategory.CategoryName = reader.GetString(3);
                    product.ProductPrice = reader.GetDecimal(4);
                    discount.Name = reader.GetString(5);
                    discount.Discount = reader.GetDecimal(6);
                    product.ProductCategoryId = reader.GetGuid(7);



                    product.Category = productCategory;
                    product.Discount = discount;

                }
                reader.Close();
            }
            connection.Close();
            return product;

        }


        public async Task<List<ProductModel>> GetProductsByCategory (Guid Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"SELECT Id, ProductName, ProductionDescription, ProductPrice, ProductCategoryId FROM Product WHERE ProductCategoryId = '{Id}';", connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            List<ProductModel> products = new List<ProductModel>();

            if (reader.HasRows)
            {

                while (await reader.ReadAsync())
                {
                    ProductModel product = new ProductModel();
                    product.Id = reader.GetGuid(0);
                    product.ProductName = reader.GetString(1);
                    product.ProductDescription = reader.GetString(2);
                    //product.Quantity = reader.GetDouble(3);
                    product.ProductPrice = reader.GetDecimal(3);

                    products.Add(product);
                }
                reader.Close();
            }
            connection.Close();
            return products;
        }





        public async Task PostProductAsync(ProductModel product)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string newProduct = $"INSERT into Product(Id, ProductName, ProductDescription, CategoryId, ProductPrice, DiscountId, Quantity, CreatedAt, ModifiedAt) VALUES" +
                    $"('{product.Id}'," +
                    $"'{product.ProductName}'," +
                    $"'{product.ProductDescription}'," +
                    $"'{product.ProductCategoryId}'," +
                    $"'{product.ProductPrice}'," +
                    $"'{product.DiscountId}'," +
                    $"'{product.Quantity}'," +
                    $"'{product.CreatedAt}'," +
                    $"'{product.ModifiedAt}')";

            using (connection)
            {
                SqlCommand command = new SqlCommand(newProduct, connection);
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(newProduct, connection);

                await command.ExecuteNonQueryAsync();
                connection.Close();
            }

        }

        public async Task UpdateProductAsync(Guid id, ProductModel product)
        {
            if (await this.GetProductByIdAsync(id) == null)
            {
                return;
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string updateProduct = $"UPDATE Product SET ProductName = '{product.ProductName}',Id = '{product.Id}'," +
                    $"ProductDescription = '{product.ProductDescription}', ProductCategoryId = '{product.ProductCategoryId}', ProductPrice = '{product.ProductPrice}'," +
                    $"DiscountId = '{product.DiscountId}', Quantity = '{product.Quantity}', CreatedAt = '{product.CreatedAt}', ModifiedAt = '{product.ModifiedAt}'";

                using (connection)
                {
                    SqlCommand command = new SqlCommand(updateProduct, connection);
                    await connection.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(updateProduct, connection);

                    await command.ExecuteNonQueryAsync();
                    connection.Close();
                }

            }
        }








        public async Task DeleteByIdAsync(Guid Id)
        {
            if (await this.GetProductByIdAsync(Id) == null)
            {
                return;
            }

            SqlConnection connection = new SqlConnection(connectionString);
            string deleteProduct = $"DELETE FROM Product WHERE ID = '{Id}';";

            using (connection)
            {
                SqlCommand command = new SqlCommand(deleteProduct, connection);
                await connection.OpenAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(deleteProduct, connection);

                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }

    }
}
