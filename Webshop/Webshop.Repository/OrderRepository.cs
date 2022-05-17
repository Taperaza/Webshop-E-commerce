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
	public class OrderRepository : IOrderRepository
	{
		public async Task CreateNewOrder(Order order, Guid paymentId)
		{
			using (SqlConnection connection = new SqlConnection(Webshop.Common.ConnectionStringProvider.GetConnectionString("WebshopDp")))
			{
				connection.Open();
				SqlTransaction transaction;
				transaction = connection.BeginTransaction("UpdateTransaction");
				SqlCommand command = new SqlCommand($"INSERT INTO Orders (Id, PaymentId, TotalPrice) VALUES ('{order.Id}', '{paymentId}', '0');", connection)
				{
					Transaction = transaction
				};

				try
				{
					await command.ExecuteNonQueryAsync();
					
					SqlDataReader reader = await command.ExecuteReaderAsync();
					command.CommandText = $"SELECT Product.Price, ProductOrder.Quantity FROM Product INNER JOIN ProductOrder ON ProductOrder.ProductID = Product.Id WHERE ProductOrder.OrderId = '{order.Id}';";
					await command.ExecuteReaderAsync();

					decimal totalPrice = 0;
					while (await reader.ReadAsync())
					{
						totalPrice += reader.GetDecimal(0) * reader.GetInt32(1);
					}
					reader.Close();

					command.CommandText = $"UPDATE Orders SET TotalPrice = {totalPrice} WHERE Orders.Id = {order.Id}";
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

		public async Task RemoveOrder(Guid orderId)
		{
			using (SqlConnection connection = new SqlConnection(Webshop.Common.ConnectionStringProvider.GetConnectionString("WebshopDp")))
			{
				connection.Open();
				SqlTransaction transaction;
				transaction = connection.BeginTransaction("DeleteTransaction");
				SqlCommand command = new SqlCommand($"DELETE FROM Orders WHERE Id = '{orderId}';", connection)
				{
					Transaction = transaction
				};

				try
				{
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
