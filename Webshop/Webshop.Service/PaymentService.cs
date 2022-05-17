using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;
using Webshop.Model.Common;
using Webshop.Repository;
using Webshop.Repository.Common;
using Webshop.Service.Common;

namespace Webshop.Service
{
    public class PaymentService : IPaymentServiceCommon
    {
        private IPaymentRepositoryCommon paymentRepository;
        public PaymentService(IPaymentRepositoryCommon paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }
        public async Task<List<PaymentModel>> GetAllAsync(Paging paging, PaymentFiltering filtering, Sorting sorting)
        {
            List<PaymentModel> pay = await paymentRepository.GetAllAsync(paging, filtering, sorting);

            return pay;
        }


        public async Task<PaymentModel> GetIdAsync(Guid Id)
        {

            PaymentModel pay = await paymentRepository.GetIdAsync(Id);

            return pay;
        }



        public async Task PostAsync(PaymentModel pay)
        {


            await paymentRepository.PostAsync(pay);
        }


        public async Task PutAsync(Guid Id, PaymentModel pay)
        {

            await paymentRepository.PutAsync(Id, pay);
        }



        public async Task DeleteIdAsync(Guid Id)
        {

            await paymentRepository.DeleteIdAsync(Id);
        }


    }
}
