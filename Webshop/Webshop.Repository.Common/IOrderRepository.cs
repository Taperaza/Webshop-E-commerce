using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;

namespace Webshop.Repository.Common
{
	public interface IOrderRepository
	{
		Task CreateNewOrder(Order order, Guid paymentId);
		Task RemoveOrder(Guid orderId);
	}
}
