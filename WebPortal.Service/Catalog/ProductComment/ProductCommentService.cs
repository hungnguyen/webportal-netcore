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
    public class ProductCommentService : Service<ProductComment, ProductCommentRequest>, IProductCommentService
    {
        public ProductCommentService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<int> DeleteByProductId(int productId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var listPF = await dbContext.ProductComments.Where(x => x.ProductID == productId).ToListAsync();
                dbContext.ProductComments.RemoveRange(listPF);
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<ProductCommentView>> GetPaging(ProductCommentSearchRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from b in dbContext.ProductComments.Include(b => b.Customer).Include(b => b.Product)
                            select b;

                if (request.ProductID != null)
                {
                    query = query.Where(b => b.ProductID == request.ProductID);
                }
                if (request.Status != null)
                {
                    query = query.Where(b => b.Status == request.Status);
                }
                if (request.FromDate != null)
                {
                    query = query.Where(b => b.DateCreated != null && b.DateCreated.Value.Date >= request.FromDate.Value.Date);
                }
                if (request.ToDate != null)
                {
                    query = query.Where(b => b.DateCreated != null && b.DateCreated.Value.Date <= request.ToDate.Value.Date);
                }

                query = query.OrderByDescending(b => b.DateCreated);

                var total = await query.CountAsync();

                if (total > request.PageSize && request.PageSize > 0)
                {
                    query = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
                }

                var data = await mapper.ProjectTo<ProductCommentView>(query)
                        .ToListAsync();

                var result = new PagedResult<ProductCommentView>()
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalRow = total,
                    Items = data
                };
                return result;
            }
        }
    }
}
