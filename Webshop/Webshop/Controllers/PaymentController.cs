using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop.Service;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Webshop.Service.Common;
using Webshop.Model;
using Webshop.Model.Common;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class PaymentController : ApiController
    {
        private IPaymentServiceCommon paymentService;
        public PaymentController(IPaymentServiceCommon paymentService)
        {
            this.paymentService = paymentService;
        }

        public async Task<List<PaymentModel>> GetAllAsync([FromUri] Paging paging, PaymentFiltering filtering, Sorting sorting)
        {

            List<PaymentModel> pay = await paymentService.GetAllAsync(paging, filtering, sorting);
            return pay;
        }


        public async Task<HttpResponseMessage> GetIdAsync(Guid Id)
        {

            var pay = await paymentService.GetIdAsync(Id);

            if (pay == null)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, pay);
            }
        }



        public async Task<HttpResponseMessage> PostAsync(PaymentRest paymentRest)
        {
            if (paymentRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, paymentRest);
            }
            else
            {
                PaymentModel pay = new PaymentModel();

                paymentRest.Id = Guid.NewGuid();
                paymentRest.CustomerId = pay.CustomerId;
                paymentRest.PaymentType = pay.PaymentType;
                paymentRest.ProviderName = pay.ProviderName;
                paymentRest.AccountNo = pay.AccountNo;
                paymentRest.CreatedAt = pay.CreatedAt;
                paymentRest.ModifiedAt = pay.ModifiedAt;

                await paymentService.PostAsync(pay);

                return Request.CreateResponse(HttpStatusCode.OK, "entry is posted");
            }
        }


        public async Task<HttpResponseMessage> PutAsync(Guid Id, [FromBody] PaymentRest paymentRest)
        {
            if (paymentRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entry found");
            }
            else
            {
                PaymentModel pay = new PaymentModel();

                pay.CustomerId = paymentRest.CustomerId;
                pay.PaymentType = paymentRest.PaymentType;
                pay.ProviderName = paymentRest.ProviderName;
                pay.AccountNo = paymentRest.AccountNo;
                pay.CreatedAt = paymentRest.CreatedAt;
                pay.ModifiedAt = paymentRest.ModifiedAt;

                await paymentService.PutAsync(Id, pay);
                return Request.CreateResponse(HttpStatusCode.OK, $"Payment of ID = {Id} has been changed; Customer to {pay.CustomerId}, PaymentType to {pay.PaymentType}, ProviderName to {pay.ProviderName} and AccountNo to {pay.AccountNo}");
            }

        }


        public async Task<HttpResponseMessage> DeleteIdAsync(Guid Id)
        {
            await paymentService.DeleteIdAsync(Id);

            return Request.CreateResponse(HttpStatusCode.OK, $"Payment with ID '{Id}'' is deleted");
        }

    }
}