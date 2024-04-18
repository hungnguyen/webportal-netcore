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
    public class ProductTypeService : Service<ProductType, ProductTypeRequest>, IProductTypeService
    {
        public ProductTypeService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {
            
        }

        public async Task<ProductType> GetByCode(string code)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var productType = await dbContext.ProductTypes.FirstOrDefaultAsync(b => b.Code.Equals(code));
                return productType;
            }
        }

        public async Task<PagedResult<ProductTypeView>> GetPaging(ProductTypeSearchRequest request)
            => await Find<ProductTypeView>(
                    b => (string.IsNullOrEmpty(request.Keyword) || b.Name.Contains(request.Keyword) || b.Code.Equals(request.Keyword)) &&
                        (!request.IsPublic.HasValue || b.IsPublic == request.IsPublic),
                    q => q.OrderBy(b => b.Name),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
