using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Webshop.Common;
using Webshop.Model;
using Webshop.Models;
using Webshop.Service;

namespace Webshop.Controllers

{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {

        
        public async Task<List<ProductModel>> GetAllAsync([FromUri]Page page, [FromUri]Sort sort, [FromUri]Filter filter )
        {
            ProductService productService = new ProductService();
            List<ProductModel> products = await productService.GetAllAsync(page, sort, filter);
            return products;
        }
        
        public async Task<HttpResponseMessage> GetIdAsync(Guid id)
        {
            ProductService productService = new ProductService();
            var product = await productService.GetProductByIdAsync(id);

            if (await productService.GetProductByIdAsync(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Product {id} not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
        }

        [HttpGet]
        [Route("api/Product/Category/{Id}")]
        public async Task<HttpResponseMessage> GetProductsByCategory(Guid Id)
        {
            ProductService productService = new ProductService();
            var products = new List<ProductModel>();
            products = await productService.GetProductsByCategory(Id);
            if (await productService.GetProductByIdAsync(Id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Product {Id} not found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }
        }

        public async Task<HttpResponseMessage> PostProductAsync([FromBody] RESTProductModel productREST)
        {
            ProductService productService = new ProductService();
            if(productREST != null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, productREST);
            }
            else
            {
                ProductModel product = new ProductModel();
                product.Id = Guid.NewGuid();
                product.ProductName = productREST.ProductName;
                product.ProductCategoryId = product.ProductCategoryId;
                //product.ProductPrice = productREST.ProductPrice;
                product.DiscountId = product.DiscountId;
                product.Quantity = productREST.Quantity;
                product.CreatedAt = productREST.CreatedAt;
                product.ModifiedAt = productREST.ModifiedAt;

                await productService.PostProductAsync(product);
                return Request.CreateResponse(HttpStatusCode.OK, "Product added");
            }
            
        }

        public async Task<HttpResponseMessage> UpdateProductAsync(Guid id, [FromBody] RESTProductModel productREST)
        {
            ProductService productService = new ProductService();

            if (productREST == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Incorrect");
            }
            else
            {
                ProductModel product = new ProductModel();


                product.ProductName = productREST.ProductName;
                product.ProductDescription = productREST.ProductDescription;
                //product.ProductPrice = productREST.ProductPrice;
                product.Quantity = productREST.Quantity;
                product.CreatedAt = productREST.CreatedAt;
                product.ModifiedAt = productREST.ModifiedAt;

                await productService.UpdateProductAsync(id, product);
                return Request.CreateResponse(HttpStatusCode.OK, "Product updated");


            }



        }
        public async Task<HttpResponseMessage> DeleteProductAsync(Guid id)
        {
            ProductService productService = new ProductService();

            if (await productService.GetProductByIdAsync(id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");
            }
            else
            {
                await productService.DeleteByIdAsync(id);

                return Request.CreateResponse(HttpStatusCode.OK, $"Product '{id}'; deleted");
            }
        }
    }
}
