using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Model
{
    public class PaymentModel
    {
        public Guid Id { get; set; } 
        public string Customer { get; set; } 
        public string PaymentType { get; set; }
        public string ProviderName { get; set; }
        public int AccountNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public PaymentModel() { }

        public PaymentModel(Guid id, string customer, string paymentType, string providerName, int accountNo, DateTime createdAt, DateTime modifiedAt)
        {
            Id = id;
            Customer = customer;
            PaymentType = paymentType;
            ProviderName = providerName;
            AccountNo = accountNo;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }
    }
}
