using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;

namespace WebPortal.Services
{
    public interface IProductInCategoryService
    {
        Task<List<ProductInCategory>> GetByProductId(int productId);
        Task<int> DeleteByProductId(int productId);
        Task<int> DeleteByCategoryId(int categoryId);
        Task<int> Create(int productId, List<int> categoryIds);
    }
}
