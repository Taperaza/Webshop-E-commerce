using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Model.Common
{
    public class PaymentFiltering
    {
        public string Customer { get; set; }
        public string PaymentType { get; set; }
        public string ProviderName { get; set; }
    }
}
