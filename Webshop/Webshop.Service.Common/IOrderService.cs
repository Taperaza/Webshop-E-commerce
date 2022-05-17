using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;

namespace Webshop.Service.Common
{
	public interface IOrderService
	{
		Task CreateNewOrder(List<ProductOrder> productOrders, Guid paymentId);
		Task RemoveOrder(Guid orderId);
	}
}
