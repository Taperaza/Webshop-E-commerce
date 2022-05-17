using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Webshop.Model;
using Webshop.Service;
using Webshop.Service.Common;

namespace Webshop.Controllers
{
	public class OrderController : ApiController
	{
		IOrderService OrderService;
		IProductOrderService ProductOrderService;

		public OrderController(IOrderService orderService, IProductOrderService productOrderService)
		{
			this.OrderService = orderService;
			this.ProductOrderService = productOrderService;
		}

		[HttpGet]
		[Route("api/orders/{orderId}")]
		public async Task<HttpResponseMessage> GetProductsOnOrder([FromUri]Guid orderId)
		{
			if (await ProductOrderService.GetProductsOnOrder(orderId) == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound, "Order not found!");
			}
			else
				return Request.CreateResponse(HttpStatusCode.OK, await ProductOrderService.GetProductsOnOrder(orderId));
		}

		[HttpPost]
		[Route("api/orders/{paymentId}")]
		public HttpResponseMessage CreateNewOrder([FromBody]List<ProductOrder> productOrders, [FromUri]Guid paymentId)
		{
			OrderService.CreateNewOrder(productOrders, paymentId);
			return Request.CreateResponse(HttpStatusCode.OK, "Order successfully added.");
		}

		[HttpPost]
		[Route("api/productOrder/{orderId}")]
		public async Task<HttpResponseMessage> CreateNewProductOrder([FromBody]ProductOrder productOrder, [FromUri]Guid orderId)
		{
			await ProductOrderService.CreateNewProductOrder(productOrder, orderId);
			return Request.CreateResponse(HttpStatusCode.OK, "New product added to the order.");
		}

		[HttpDelete]
		[Route("api/orders/{orderId}")]
		public async Task<HttpResponseMessage> RemoveOrder([FromUri]Guid orderId)
		{
			await OrderService.RemoveOrder(orderId);
			return Request.CreateResponse(HttpStatusCode.OK, "Order successfully removed.");
		}

		[HttpDelete]
		[Route("api/productOrder/{orderId}")]
		public async Task<HttpResponseMessage> RemoveProductFromOrder([FromBody]ProductOrder productOrder, [FromUri]Guid orderId)
		{
			await ProductOrderService.RemoveProductFromOrder(productOrder, orderId);
			return Request.CreateResponse(HttpStatusCode.OK, "Product removed from the order.");
		}
	}
}