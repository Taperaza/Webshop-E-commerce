using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Model
{
	public class ProductOrder
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public Guid OrderId { get; set; }
		public int Quantity { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
		public DateTime DeletedAt { get; set; }

		public ProductOrder() { }
	}
}
