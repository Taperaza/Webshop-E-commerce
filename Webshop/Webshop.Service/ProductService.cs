using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;
using Webshop.Repository;
using Webshop.Service.Common;
namespace Webshop.Service
{
    public class ProductService : IProductService
    {
        readonly List<ProductModel> products = new List<ProductModel>();

        public async Task<List<ProductModel>> GetAllAsync(Page page, Sort sort, Filter filter)
        {
            ProductRepository productRepository = new ProductRepository();
            return await productRepository.GetAllAsync(page, sort, filter);
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            ProductRepository productRepository = new ProductRepository();
            return await productRepository.GetProductByIdAsync(id);
        }

        public async Task<List<ProductModel>> GetProductsByCategory(Guid Id)
        {
            ProductRepository productRepository = new ProductRepository();
            return await productRepository.GetProductsByCategory(Id);
        }

        public async Task PostProductAsync(ProductModel product)
        {
            ProductRepository productRepository = new ProductRepository();
            await productRepository.PostProductAsync(product);
        }

        public async Task UpdateProductAsync(Guid id, ProductModel product)
        {
            ProductRepository productRepository = new ProductRepository();
            await productRepository.UpdateProductAsync(id, product);
        }
        public async Task DeleteByIdAsync(Guid Id)
        {
            ProductRepository productRepository = new ProductRepository();
            await productRepository.DeleteByIdAsync(Id);
        }
    }
}
