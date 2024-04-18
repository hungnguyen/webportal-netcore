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
    public class ProductVoteService : Service<ProductVote, ProductVoteRequest>, IProductVoteService
    {
        public ProductVoteService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {
           
        }

        public async Task<int> DeleteByProductId(int productId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var listPF = await dbContext.ProductVotes.Where(x => x.ProductID == productId).ToListAsync();
                dbContext.ProductVotes.RemoveRange(listPF);
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<ProductVoteView>> GetPaging(ProductVoteSearchRequest request)
            => await Find<ProductVoteView>(
                    b => request.ProductID == null || b.ProductID == request.ProductID,
                    q => q.OrderByDescending(b => b.DateCreated),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
