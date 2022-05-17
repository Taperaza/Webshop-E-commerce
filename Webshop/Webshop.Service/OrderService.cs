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
	public class OrderService : IOrderService
	{
		IOrderRepository OrderRepository;
		IProductOrderRepository ProductOrderRepository;

		public OrderService(IOrderRepository orderRepository, IProductOrderRepository productOrderRepository)
		{
			this.OrderRepository = orderRepository;
			this.ProductOrderRepository = productOrderRepository;
		}

		public async Task CreateNewOrder(List<ProductOrder> productOrders, Guid paymentId)
		{
			Order order = new Order
			{
				Id = new Guid()
			};

			foreach (ProductOrder productOrder in productOrders)
			{
				await ProductOrderRepository.CreateNewProductOrder(productOrder, order.Id);
			}

			await OrderRepository.CreateNewOrder(order, paymentId);
		}

		public async Task RemoveOrder(Guid orderId)
		{
			foreach (ProductOrder productOrder in await ProductOrderRepository.GetProductsOnOrder(orderId))
				await ProductOrderRepository.RemoveProductFromOrder(productOrder, orderId);
			await OrderRepository.RemoveOrder(orderId);
		}

	}
}
