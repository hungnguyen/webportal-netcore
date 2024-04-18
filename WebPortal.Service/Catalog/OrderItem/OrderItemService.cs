using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Services.Common;
using WebPortal.Utilities.Exeptions;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace WebPortal.Services
{
    public class OrderItemService : Service<OrderItem, OrderItemRequest>, IOrderItemService
    {
        public OrderItemService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<List<OrderItem>> BulkCreate(List<OrderItemRequest> orderItemRequests)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var orderItems = mapper.Map<List<OrderItem>>(orderItemRequests);
                dbContext.OrderItems.AddRange(orderItems);
                await dbContext.SaveChangesAsync();
                return orderItems;
            }
        }

        public async Task<List<OrderItemView>> GetByOrderId(int orderId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from oi in dbContext.OrderItems
                            where oi.OrderID == orderId
                            select oi;

                query = query.OrderBy(b => b.ID);

                var data = await mapper.ProjectTo<OrderItemView>(query)
                        .ToListAsync();

                return data;
            }
        }

        public async Task<PagedResult<OrderItemView>> GetPaging(OrderItemSearchRequest request)
            => await Find<OrderItemView>(
                    b => b.OrderID == request.OrderID,
                    q => q.OrderBy(b => b.ID),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
