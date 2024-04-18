using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IOrderItemService : IService<OrderItem, OrderItemRequest>
    {
        Task<PagedResult<OrderItemView>> GetPaging(OrderItemSearchRequest request);
        Task<List<OrderItemView>> GetByOrderId(int orderId);
        Task<List<OrderItem>> BulkCreate(List<OrderItemRequest> orderItemRequests);
    }
}
