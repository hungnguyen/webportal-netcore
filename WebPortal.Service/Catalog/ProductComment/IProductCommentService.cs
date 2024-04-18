using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IProductCommentService : IService<ProductComment, ProductCommentRequest>
    {
        Task<PagedResult<ProductCommentView>> GetPaging(ProductCommentSearchRequest request);
        Task<int> DeleteByProductId(int productId);
    }
}
