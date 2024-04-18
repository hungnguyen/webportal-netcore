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
    public class PageViewService : Service<PageView, PageViewRequest>, IPageViewService
    {
        public PageViewService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<PagedResult<PageViewView>> GetPaging(PageViewSearchRequest request)
            => await Find<PageViewView>(
                    b => b.WebsiteID == request.WebsiteID &&
                        (request.FromDate == null || (b.DateReported != null && b.DateReported.Value.Date >= request.FromDate.Value.Date)) &&
                        (request.ToDate == null || (b.DateReported != null && b.DateReported.Value.Date <= request.ToDate.Value.Date)),
                    q => q.OrderByDescending(b => b.DateReported),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        

        public async Task<int> Increase(int websiteId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var pageViewRequest = new PageViewRequest();
                var pageView = await dbContext.PageViews.FirstOrDefaultAsync(pv => pv.WebsiteID == websiteId && pv.DateReported != null && pv.DateReported.Value.Date == DateTime.Now.Date);
                if (pageView != null)
                {
                    mapper.Map(pageView, pageViewRequest);
                    pageViewRequest.Total += 1;

                    await Update(pageView.ID, pageViewRequest);
                }
                else
                {
                    pageViewRequest = new PageViewRequest()
                    {
                        WebsiteID = websiteId,
                        DateReported = DateTime.Now,
                        Total = 1
                    };

                    await Create(pageViewRequest);
                }
                return pageViewRequest.Total;
            }
        }
    }
}
