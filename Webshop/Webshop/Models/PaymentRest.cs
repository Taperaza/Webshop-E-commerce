using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class PaymentRest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string PaymentType { get; set; }
        public string ProviderName { get; set; }
        public int AccountNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}