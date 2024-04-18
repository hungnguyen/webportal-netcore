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
    public class PromotionService : Service<Promotion, PromotionRequest>, IPromotionService
    {
        public PromotionService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {
            
        }

        public async Task<PagedResult<PromotionView>> GetPaging(PromotionSearchRequest request)
            => await Find<PromotionView>(
                    b => (string.IsNullOrEmpty(request.Keyword) || b.Name.Contains(request.Keyword)) &&
                        (request.Status == null || b.Status == request.Status) &&
                        (request.FromDate == null || (b.FromDate != null && b.FromDate.Value.Date <= request.FromDate.Value.Date)) &&
                        (request.ToDate == null || (b.ToDate != null && b.ToDate.Value.Date >= request.ToDate.Value.Date)),
                    q => q.OrderBy(b => b.Name),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
       
    }
}
