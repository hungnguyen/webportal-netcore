using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IProductTypeService : IService<ProductType, ProductTypeRequest>
    {
        Task<PagedResult<ProductTypeView>> GetPaging(ProductTypeSearchRequest request);
        Task<ProductType> GetByCode(string code);
    }
}
