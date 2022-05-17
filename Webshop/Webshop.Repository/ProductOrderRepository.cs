using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;
using Webshop.Repository.Common;

namespace Webshop.Repository
{
	public class ProductOrderRepository : IProductOrderRepository
	{
		public async Task<List<ProductOrder>> GetProductsOnOrder(Guid orderId)
		{
			using (SqlConnection connection = new SqlConnection(Webshop.Common.ConnectionStringProvider.GetConnectionString("WebshopDp")))
			{
				connection.Open();
				SqlCommand sqlCommand = new SqlCommand($"SELECT Id, ProductId, Quantity, OrderId FROM dbo.ProductOrder WHERE OrderId = '{orderId}';", connection);
				SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
				List<ProductOrder> tempProductOrders = new List<ProductOrder>();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						ProductOrder productOrder = new ProductOrder
						{
							Id = reader.GetGuid(0),
							ProductId = reader.GetGuid(1),
							Quantity = reader.GetInt32(2),
							OrderId = reader.GetGuid(3)
						};
						tempProductOrders.Add(productOrder);
					}
					reader.Close();
					return tempProductOrders;
				}
				else
					return null;
			}
		}

		public async Task CreateNewProductOrder(ProductOrder productOrder, Guid orderId)
		{
			using (SqlConnection connection = new SqlConnection(Webshop.Common.ConnectionStringProvider.GetConnectionString("WebshopDp")))
			{
				connection.Open();
				SqlTransaction transaction;
				transaction = connection.BeginTransaction("UpdateTransaction");
				SqlCommand command = new SqlCommand($"SELECT Quantity FROM dbo.Product WHERE Product.Id = '{productOrder.ProductId}';", connection)
				{
					Transaction = transaction
				};
				
				try
				{
					SqlDataReader reader = await command.ExecuteReaderAsync();
					await reader.ReadAsync();
					if ((reader.GetDecimal(0) == 0) && (reader.GetDecimal(0) < productOrder.Quantity))
					{
						reader.Close();
						return; //invalid request; not enough products in stock
					}
					else
					{
						reader.Close();
						command.CommandText = $"INSERT INTO dbo.ProductOrder (ProductId, OrderID, Quantity) VALUES ('{productOrder.ProductId}', '{orderId}', '{productOrder.Quantity}');";
						await command.ExecuteNonQueryAsync();

						command.CommandText = $"UPDATE dbo.Product SET Product.Quantity -= '{productOrder.Quantity}' WHERE Product.Id = '{productOrder.ProductId}';";
						await command.ExecuteNonQueryAsync();

						transaction.Commit();
					}
				}
				catch (Exception commitException)
				{
					System.Diagnostics.Debug.WriteLine("Commit Exception Type: {0}", commitException.GetType());
					System.Diagnostics.Debug.WriteLine("  Message: {0}", commitException.Message);
					try
					{
						transaction.Rollback();
					}
					catch (Exception rollbackException)
					{
						System.Diagnostics.Debug.WriteLine("Rollback Exception Type: {0}", rollbackException.GetType());
						System.Diagnostics.Debug.WriteLine("  Message: {0}", rollbackException.Message);
					}
				}
				connection.Close();
			}
		}

		public async Task ChangeProductQuantity(ProductOrder productOrder, Guid orderId)
		{
			using (SqlConnection connection = new SqlConnection(Webshop.Common.ConnectionStringProvider.GetConnectionString("WebshopDp")))
			{
				connection.Open();
				SqlTransaction transaction;
				transaction = connection.BeginTransaction("UpdateTransaction");
				SqlCommand command = new SqlCommand($"SELECT Product.Quantity, Product.Price FROM Product WHERE Product.Id = '{productOrder.ProductId}';", connection)
				{
					Transaction = transaction
				};
				decimal totalPrice = 0;

				try
				{
					SqlDataReader reader = await command.ExecuteReaderAsync();
					await reader.ReadAsync();
					if ((reader.GetDecimal(0) == 0) && (reader.GetDecimal(0) < productOrder.Quantity))
					{
						reader.Close();
						return; //invalid request; not ehnough products in stock
					}
					else
					{
						while (await reader.ReadAsync())
						{
							totalPrice += reader.GetDecimal(0) * reader.GetInt32(1);
						}
						reader.Close();

						command.CommandText = $"UPDATE ProductOrder SET Quantity = '{productOrder.Quantity}' WHERE OrderId = '{orderId}' AND ProductId = '{productOrder.ProductId}';";
						await command.ExecuteNonQueryAsync();

						command.CommandText = $"UPDATE Orders SET TotalPrice = {totalPrice} WHERE Orders.Id = {orderId}";
						await command.ExecuteNonQueryAsync();

						transaction.Commit();
					}
				}
				catch (Exception commitException)
				{
					System.Diagnostics.Debug.WriteLine("Commit Exception Type: {0}", commitException.GetType());
					System.Diagnostics.Debug.WriteLine("  Message: {0}", commitException.Message);
					try
					{
						transaction.Rollback();
					}
					catch (Exception rollbackException)
					{
						System.Diagnostics.Debug.WriteLine("Rollback Exception Type: {0}", rollbackException.GetType());
						System.Diagnostics.Debug.WriteLine("  Message: {0}", rollbackException.Message);
					}
				}

				connection.Close();
			}
		}

		public async Task RemoveProductFromOrder(ProductOrder productOrder, Guid orderId)
		{
			using (SqlConnection connection = new SqlConnection(Webshop.Common.ConnectionStringProvider.GetConnectionString("WebshopDp")))
			{
				connection.Open();
				SqlTransaction transaction;
				transaction = connection.BeginTransaction("DeleteTransaction");
				SqlCommand command = new SqlCommand($"SELECT Product.ProductPrice, ProductOrder.Quantity FROM Product INNER JOIN ProductOrder ON ProductOrder.ProductID = Product.Id WHERE ProductOrder.OrderId = '{orderId}' AND ProductOrder.Id = '{productOrder.Id}'; ", connection)
				{
					Transaction = transaction
				};
				decimal totalPrice;
				int tempQuantity;

				try
				{
					SqlDataReader reader = await command.ExecuteReaderAsync();
					await reader.ReadAsync();
					tempQuantity = reader.GetInt32(1);
					totalPrice = reader.GetDecimal(0) * reader.GetInt32(1);
					reader.Close();


					command.CommandText = $"UPDATE Product SET Product.Quantity += '{tempQuantity}' WHERE Product.Id = '{productOrder.ProductId}';";
					await command.ExecuteNonQueryAsync();

					command.CommandText = $"DELETE FROM ProductOrder WHERE Id = '{productOrder.Id}' AND OrderID = '{orderId}';";
					await command.ExecuteNonQueryAsync();

					command.CommandText = $"UPDATE Orders SET TotalPrice -= '{totalPrice}' WHERE Orders.Id = '{orderId}'";
					await command.ExecuteNonQueryAsync();

					transaction.Commit();
				}
				catch (Exception commitException)
				{
					System.Diagnostics.Debug.WriteLine("Commit Exception Type: {0}", commitException.GetType());
					System.Diagnostics.Debug.WriteLine("  Message: {0}", commitException.Message);
					try
					{
						transaction.Rollback();
					}
					catch (Exception rollbackException)
					{
						System.Diagnostics.Debug.WriteLine("Rollback Exception Type: {0}", rollbackException.GetType());
						System.Diagnostics.Debug.WriteLine("  Message: {0}", rollbackException.Message);
					}
				}

				connection.Close();
			}
		}
	}
}
