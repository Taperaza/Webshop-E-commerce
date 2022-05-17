using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Webshop.Common;
using Webshop.Model;
using Webshop.Models;
using Webshop.Service;

namespace Webshop.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("api/categories/")]
        public async Task<List<ProductCategoryModel>> GetCategoryAsync([FromUri] Page page, [FromUri] Sort sort, [FromUri] Filter filter)
        {
            ProductCategoryService productCategoryService = new ProductCategoryService();
            List<ProductCategoryModel> categorys = await productCategoryService.GetCategoryAsync(page, sort, filter);
            return categorys;
        }

        public async Task<HttpResponseMessage> GetCategoryByIdAsync(Guid id)
        {
            ProductCategoryService productCategoryService = new ProductCategoryService();
            var category = await productCategoryService.GetCategoryByIdAsync(id);

            if (await productCategoryService.GetCategoryByIdAsync(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Category {id} not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
        }

        public async Task<HttpResponseMessage> PostCategoryAsync([FromBody] RESTProductCategoryModel categoryREST)
        {
            ProductCategoryService productCategoryService = new ProductCategoryService();
            ProductCategoryModel category = new ProductCategoryModel()
            {
                CategoryId = categoryREST.CategoryId,
                CategoryName = categoryREST.CategoryName,
                CategoryDescription = categoryREST.CategoryDescription,
                CreatedAt = categoryREST.CreatedAt,
                ModifiedAt = categoryREST.ModifiedAt
            };
            await productCategoryService.PostCategoryAsync(category);
            return Request.CreateResponse(HttpStatusCode.OK, "Category added");
        }

        public async Task<HttpResponseMessage> UpdateCategoryAsync(Guid id, [FromBody] RESTProductCategoryModel categoryREST)
        {
            ProductCategoryService productCategoryService = new ProductCategoryService();

            if (categoryREST == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                ProductCategoryModel category = new ProductCategoryModel();

                category.CategoryName = categoryREST.CategoryName;
                category.CategoryDescription = categoryREST.CategoryDescription;
                category.CreatedAt = categoryREST.CreatedAt;
                category.ModifiedAt = categoryREST.ModifiedAt;

                

                await productCategoryService.UpdateCategoryAsync(id, category);

                return Request.CreateResponse(HttpStatusCode.OK, "Category updated");
            }



        }
        public async Task<HttpResponseMessage> DeleteCategoryAsync(Guid id)
        {
            ProductCategoryService productCategoryService = new ProductCategoryService();

            if (await productCategoryService.GetCategoryByIdAsync(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Category not found.");
            }
            else
            {
                await productCategoryService.DeleteByIdAsync(id);

                return Request.CreateResponse(HttpStatusCode.OK, $"Category '{id}'; deleted");
            }
        }
    }
}