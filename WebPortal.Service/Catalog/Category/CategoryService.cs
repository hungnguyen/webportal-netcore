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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebPortal.Services
{
    public class CategoryService : Service<Category, CategoryRequest>, ICategoryService
    {
        public CategoryService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<Category> GetByUrlName(string urlName)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                return await dbContext.Categories.Where(b => b.UrlName.Equals(urlName)).FirstOrDefaultAsync();
            }
        }

        public async Task<PagedResult<CategoryView>> GetPaging(CategorySearchRequest request)
            => await Find<CategoryView>(
                    b => b.LanguageID == request.LanguageId && b.WebsiteID == request.WebsiteID &&
                        (string.IsNullOrEmpty(request.Keyword) || b.Name.Contains(request.Keyword)) &&
                        (string.IsNullOrEmpty(request.TypeCode) || b.TypeCode.Equals(request.TypeCode)) &&
                        (request.Status == null || b.Status == request.Status) &&
                        (request.IsOnTop == null || b.IsOnTop == request.IsOnTop) &&
                        (request.IsOnRight == null || b.IsOnRight == request.IsOnRight) &&
                        (request.IsOnBottom == null || b.IsOnBottom == request.IsOnBottom) &&
                        (request.IsOnLeft == null || b.IsOnLeft == request.IsOnLeft) &&
                        (request.IsOnCenter == null || b.IsOnCenter == request.IsOnCenter) &&
                        (request.ParentID == null || b.ParentID == request.ParentID) &&
                        (request.InIds == null || request.InIds.Length == 0 || request.InIds.Any(i => i == b.ID)),
                    q => q.OrderBy(b => b.ParentID).ThenBy(b => b.OrderNumber),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
