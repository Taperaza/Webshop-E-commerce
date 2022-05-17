using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model.Common;
using Webshop.Model;

namespace Webshop.Repository.Common
{
    public interface IPaymentRepositoryCommon
    {
        Task<List<PaymentModel>> GetAllAsync(Paging paging, PaymentFiltering filtering, Sorting sorting);
        Task<PaymentModel> GetIdAsync(Guid id);
        Task PostAsync(PaymentModel pay);
        Task PutAsync(Guid id, PaymentModel pay);
        Task DeleteIdAsync(Guid id);
    }
}
