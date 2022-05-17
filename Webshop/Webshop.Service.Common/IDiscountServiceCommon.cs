using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model.Common;
using Webshop.Model;

namespace Webshop.Service.Common
{
    public interface IDiscountServiceCommon
    {
        Task<List<DiscountModel>> GetAllAsync(Paging paging, Sorting sorting);
        Task<DiscountModel> GetIdAsync(Guid id);
        Task PostAsync(DiscountModel disc);
        Task PutAsync(Guid id, DiscountModel disc);
        Task DeleteIdAsync(Guid id);
    }
}
