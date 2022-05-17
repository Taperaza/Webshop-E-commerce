using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;
using Webshop.Repository;

namespace Webshop.Service
{
    public class ProductCategoryService
    {
        readonly List<ProductCategoryModel> categorys = new List<ProductCategoryModel>();

        public async Task<List<ProductCategoryModel>> GetCategoryAsync(Page page, Sort sort, Filter filter)
        {
            ProductCategoryRepository productCategoryRepository = new ProductCategoryRepository();
            return await productCategoryRepository.GetCategoryAsync(page, sort, filter);
        }

        public async Task<ProductCategoryModel> GetCategoryByIdAsync(Guid id)
        {
            ProductCategoryRepository productCategoryRepository = new ProductCategoryRepository();
            return await productCategoryRepository.GetCategoryByIdAsync(id);
        }


        public async Task PostCategoryAsync(ProductCategoryModel category)
        {
            ProductCategoryRepository productCategoryRepository = new ProductCategoryRepository();
            await productCategoryRepository.PostCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Guid id, ProductCategoryModel category)
        {
            ProductCategoryRepository productCategoryRepository = new ProductCategoryRepository();
            await productCategoryRepository.UpdateCategoryAsync(id, category);
        }
        public async Task DeleteByIdAsync(Guid Id)
        {
            ProductCategoryRepository productCategoryRepository = new ProductCategoryRepository();
            await productCategoryRepository.DeleteByIdAsync(Id);
        }
    }
}
