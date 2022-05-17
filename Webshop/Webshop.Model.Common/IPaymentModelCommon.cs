using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Model.Common
{
    public interface IPaymentModelCommon
    {
        Guid Id { get; set; }
        Guid CustomerId { get; set; }
        string PaymentType { get; set; }
        string ProviderName { get; set; }
        int AccountNo { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}
