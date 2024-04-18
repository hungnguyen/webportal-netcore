using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.Services
{
    public interface ICategoryService : IService<Category, CategoryRequest>
    {
        Task<PagedResult<CategoryView>> GetPaging(CategorySearchRequest request);
        Task<Category> GetByUrlName(string urlName);        
    }
}
