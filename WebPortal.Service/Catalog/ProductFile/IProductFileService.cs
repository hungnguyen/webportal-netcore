using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IProductFileService : IService<ProductFile, ProductFileRequest>
    {
        Task<PagedResult<ProductFileView>> GetPaging(ProductFileSearchRequest request);
        Task<List<ProductFileView>> GetByProductId(int productId);
        Task<int> DeleteByProductId(int productId);
    }
}
