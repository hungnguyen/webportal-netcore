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
    public class LanguageService : Service<Language, LanguageRequest>, ILanguageService
    {
        public LanguageService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<PagedResult<LanguageView>> GetPaging(LanguageSearchRequest request)
            => await Find<LanguageView>(
                    b => (b.WebsiteID == request.WebsiteID) &&
                        (string.IsNullOrEmpty(request.Keyword) || b.Code.Contains(request.Keyword) ||
                                        b.Name.Contains(request.Keyword)),
                    q => q.OrderBy(b => b.Name),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
