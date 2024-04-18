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
    public class CartService : Service<Cart, CartRequest>, ICartService
    {
        
        public CartService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {
           
        }

        public async Task<List<Cart>> GetBySessionID(Guid SessionID)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                return await dbContext.Carts.Include(c => c.Product).Where(c => c.SessionID == SessionID).ToListAsync();
            }
        }

        public async Task<PagedResult<CartView>> GetPaging(CartSearchRequest request)
            => await Find<CartView>(
                    b => (request.CustomerID == 0 || b.CustomerID == request.CustomerID) &&
                        (request.SessionID == Guid.Empty || b.SessionID == request.SessionID),
                    q => q.OrderBy(b => b.ID),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
