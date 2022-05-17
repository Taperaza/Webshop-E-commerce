using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;
using Webshop.Repository;
using Webshop.Repository.Common;
using Webshop.Service.Common;

namespace Webshop.Service
{
	public class ProductOrderService : IProductOrderService
	{
		IProductOrderRepository ProductOrderRepo;

		public ProductOrderService (IProductOrderRepository productOrderRepository)
		{
			this.ProductOrderRepo = productOrderRepository;
		}

		public async Task<List<ProductOrder>> GetProductsOnOrder(Guid orderId)
		{
			return await ProductOrderRepo.GetProductsOnOrder(orderId);
		}

		public async Task CreateNewProductOrder(ProductOrder productOrder, Guid orderId)
		{
			await ProductOrderRepo.CreateNewProductOrder(productOrder, orderId);
		}

		public async Task ChangeProductQuantity(ProductOrder productOrder, Guid orderId)
		{
			await ProductOrderRepo.ChangeProductQuantity(productOrder, orderId);
		}

		public async Task RemoveProductFromOrder(ProductOrder productOrder, Guid orderId)
		{
			await ProductOrderRepo.RemoveProductFromOrder(productOrder, orderId);
		}
	}
}
