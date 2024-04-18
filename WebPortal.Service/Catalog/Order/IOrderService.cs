using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IOrderService : IService<Order, OrderRequest>
    {
        Task<PagedResult<OrderView>> GetPaging(OrderSearchRequest request);
        Task<int> GetCountByCustomerId(int id);
        Task<int> GetCountByProductId(int id);
        Task<int> Count(OrderSearchRequest request);
    }
}
