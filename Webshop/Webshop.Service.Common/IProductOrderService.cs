using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;

namespace Webshop.Service.Common
{
	public interface IProductOrderService
	{
		Task<List<ProductOrder>> GetProductsOnOrder(Guid orderId);
		Task CreateNewProductOrder(ProductOrder productOrder, Guid orderId);
		Task ChangeProductQuantity(ProductOrder productOrder, Guid orderId);
		Task RemoveProductFromOrder(ProductOrder productOrder, Guid orderId);
	}
}
