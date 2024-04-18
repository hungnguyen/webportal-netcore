using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Utilities.Exeptions;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WebPortal.Services
{
    public class OrderService : Service<Order, OrderRequest>, IOrderService
    {
        public OrderService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {
            
        }

        public async Task<int> Count(OrderSearchRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from b in dbContext.Orders
                            where b.WebsiteID == request.WebsiteID
                            select b;

                if (request.OrderStatus != null)
                {
                    query = query.Where(b => b.OrderStatus == request.OrderStatus);
                }
                if (request.PayStatus != null)
                {
                    query = query.Where(b => b.PayStatus == request.PayStatus);
                }
                if (request.FromDate != null)
                {
                    query = query.Where(b => b.OrderDate != null && b.OrderDate.Value.Date >= request.FromDate.Value.Date);
                }
                if (request.ToDate != null)
                {
                    query = query.Where(b => b.OrderDate != null && b.OrderDate.Value.Date <= request.ToDate.Value.Date);
                }
                if (request.CustomerID != 0)
                {
                    query = query.Where(b => b.CustomerID == request.CustomerID);
                }
                if (request.SaleID != Guid.Empty && request.SaleID != Guid.Empty)
                {
                    query = query.Where(b => b.SaleID == request.SaleID);
                }

                return await query.CountAsync();
            }
        }

        public async Task<int> GetCountByCustomerId(int id)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                return await dbContext.Orders.Where(o => o.CustomerID == id).CountAsync();
            }
        }

        public async Task<int> GetCountByProductId(int id)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from o in dbContext.Orders
                            join oi in dbContext.OrderItems on o.ID equals oi.OrderID
                            where oi.ProductID == id
                            select o;

                return await query.CountAsync();
            }
        }

        public async Task<PagedResult<OrderView>> GetPaging(OrderSearchRequest request)
            => await Find<OrderView>(
                    b => b.WebsiteID == request.WebsiteID &&
                        (request.OrderID == null || b.ID == request.OrderID) &&
                        (request.OrderStatus == null || b.OrderStatus == request.OrderStatus) &&
                        (request.PayStatus == null || b.PayStatus == request.PayStatus) &&
                        (request.FromDate == null || (b.OrderDate != null && b.OrderDate.Value.Date >= request.FromDate.Value.Date)) &&
                        (request.ToDate == null || (b.OrderDate != null && b.OrderDate.Value.Date <= request.ToDate.Value.Date)) &&
                        (request.CustomerID == 0 || b.CustomerID == request.CustomerID) &&
                        (request.SaleID == Guid.Empty || b.SaleID == request.SaleID),
                    q => q.OrderByDescending(b => b.OrderDate),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
