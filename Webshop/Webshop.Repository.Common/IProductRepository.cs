using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Common;
using Webshop.Model;

namespace Webshop.Repository.Common
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllAsync(Page page, Sort sort, Filter filter);
        Task<ProductModel> GetProductByIdAsync(Guid id);  
        Task PostProductAsync(ProductModel product);
        Task UpdateProductAsync (Guid id, ProductModel product);   
        Task DeleteByIdAsync (Guid id);
    }
}
