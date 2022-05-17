using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Webshop.Model;
using Webshop.Model.Common;
using Webshop.Models;
using Webshop.Service;
using Webshop.Service.Common;

namespace Webshop.Controllers
{
    public class DiscountController : ApiController
    {
        private IDiscountServiceCommon discountService;
        public DiscountController(IDiscountServiceCommon discountService)
        {
            this.discountService = discountService;
        }


        public async Task<List<DiscountModel>> GetAllAsync([FromUri] Paging paging, Sorting sorting)
        {

            List<DiscountModel> disc = await discountService.GetAllAsync(paging, sorting);

            return disc;


        }


        public async Task<HttpResponseMessage> GetIdAsync(Guid Id)
        {


            var disc = await discountService.GetIdAsync(Id);

            if (await discountService.GetIdAsync(Id) == null)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound);


            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.OK, disc);
            }
        }



        public async Task<HttpResponseMessage> PostAsync(DiscountRest discountRest)
        {

            if (discountRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, discountRest);
            }
            else
            {
                DiscountModel disc = new DiscountModel();

                discountRest.Id = Guid.NewGuid();
                discountRest.DiscountName = disc.DiscountName;
                discountRest.Discount = disc.Discount;
                discountRest.Active = disc.Active;
                discountRest.CreatedAt = disc.CreatedAt;
                discountRest.ModifiedAt = disc.ModifiedAt;

                await discountService.PostAsync(disc);

                return Request.CreateResponse(HttpStatusCode.OK, "entry is posted");
            }

        }


        public async Task<HttpResponseMessage> PutAsync(Guid Id, [FromBody] DiscountRest discountRest)
        {


            if (discountRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entry found");
            }
            else
            {
                DiscountModel disc = new DiscountModel();

                disc.DiscountName = discountRest.DiscountName;
                disc.Discount = discountRest.Discount;
                disc.Active = discountRest.Active;
                disc.CreatedAt = discountRest.CreatedAt;
                disc.ModifiedAt = discountRest.ModifiedAt;

                await discountService.PutAsync(Id, disc);
                return Request.CreateResponse(HttpStatusCode.OK, $"Discount of ID = {Id} has been changed; DiscountName to {disc.DiscountName}, Discount to {disc.Discount} and Active to {disc.Active}");
            }
        }


        public async Task<HttpResponseMessage> DeleteIdAsync(Guid Id)

        {

            if (await discountService.GetIdAsync(Id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Discount not found");
            }

            else
            {
                await discountService.DeleteIdAsync(Id);
                return Request.CreateResponse(HttpStatusCode.OK, $"Discount with ID '{Id}'' is deleted");
            }

        }

    }
}