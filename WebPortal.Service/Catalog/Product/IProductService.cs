using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IProductService : IService<Product, ProductRequest>
    {
        Task<PagedResult<ProductView>> GetPaging(ProductSearchRequest request);
        Task<Product> GetOneByCategoryId(ProductSearchRequest request);
        Task<Product> GetByUrlName(string urlName);
        Task<Category> GetPrimaryCategory(int id);
        Task<int> Count(ProductSearchRequest request);
    }
}
