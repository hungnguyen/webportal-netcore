using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebPortal.Utilities.Exeptions;
using WebPortal.Services.Common;
using Microsoft.Extensions.DependencyInjection;

namespace WebPortal.Services
{
    public class BannerService : Service<Banner, BannerRequest>, IBannerService
    {
        public BannerService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<PagedResult<BannerView>> GetPaging(BannerSearchRequest request)
            => await Find<BannerView>(
                b => (b.LanguageID == request.LanguageId && b.WebsiteID == request.WebsiteID) &&
                    (request.CategoryID == null || b.InCategories.StartsWith($"{request.CategoryID},") ||
                                                b.InCategories.Contains($",{request.CategoryID},") ||
                                                b.InCategories.EndsWith($",{request.CategoryID}")) &&
                    (request.Status == null || b.Status == request.Status) &&
                    (request.Position == null || b.Position == request.Position) &&
                    (string.IsNullOrEmpty(request.Keyword) || b.Name.Contains(request.Keyword)),
                q => q.OrderByDescending(b => b.DateUpdated),
                pageIndex: request.PageIndex, pageSize: request.PageSize
            );
        
    }
}
