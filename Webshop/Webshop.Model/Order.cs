using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Model
{
	public class Order
	{
		public Guid Id { get; set; }
		public Guid PaymentId { get; set; }
		public decimal Price { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
		public DateTime DeletedAt { get; set; }

		public Order() { }
		public Order(PaymentModel payment)
		{
			this.Id = new Guid();
			this.PaymentId = payment.Id;
			this.Price = 0;
		}
	}
}
