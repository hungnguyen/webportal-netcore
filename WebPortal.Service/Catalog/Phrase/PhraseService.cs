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
    public class PhraseService : Service<Phrase, PhraseRequest>, IPhraseService
    {
        public PhraseService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<PagedResult<PhraseView>> GetPaging(PhraseSearchRequest request)
            => await Find<PhraseView>(
                    b => b.WebsiteID == request.WebsiteID && b.LanguageID == request.LanguageId &&
                        (string.IsNullOrEmpty(request.Keyword) || b.Key.Contains(request.Keyword) || b.Value.Contains(request.Keyword)),
                    q => q.OrderByDescending(b => b.IsPin).ThenBy(b => b.Key),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
