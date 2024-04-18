using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.Services
{
    public interface ICartService : IService<Cart,CartRequest>
    {
        Task<PagedResult<CartView>> GetPaging(CartSearchRequest request);
        Task<List<Cart>> GetBySessionID(Guid SessionID);
    }
}
