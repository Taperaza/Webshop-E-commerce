using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Model.Common
{
    public interface IDiscountModelCommon
    {
        Guid Id { get; set; }
        string DiscountName { get; set; }
        int Discount { get; set; }
        bool Active { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}
