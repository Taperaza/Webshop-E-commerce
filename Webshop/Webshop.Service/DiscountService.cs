using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;
using Webshop.Model.Common;
using Webshop.Repository.Common;
using Webshop.Service.Common;

namespace Webshop.Service
{
    public class DiscountService : IDiscountServiceCommon
    {

        private IDiscountRepositoryCommon discountRepository;
        public DiscountService(IDiscountRepositoryCommon discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        public async Task<List<DiscountModel>> GetAllAsync(Paging paging, Sorting sorting)
        {
            List<DiscountModel> disc = await discountRepository.GetAllAsync(paging, sorting);
            return disc;

        }


        public async Task<DiscountModel> GetIdAsync(Guid Id)
        {

            return await discountRepository.GetIdAsync(Id);
        }



        public async Task PostAsync(DiscountModel disc)
        {

            await discountRepository.PostAsync(disc);
        }


        public async Task PutAsync(Guid Id, DiscountModel disc)
        {

            await discountRepository.PutAsync(Id, disc);
        }



        public async Task DeleteIdAsync(Guid Id)
        {

            await discountRepository.DeleteIdAsync(Id);
        }


    }
}
