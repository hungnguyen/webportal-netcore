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
    public class MailBoxService : Service<MailBox,MailBoxRequest>, IMailBoxService
    {
        public MailBoxService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {
            
        }

        public async Task<PagedResult<MailBoxView>> GetPaging(MailBoxSearchRequest request)
            => await Find<MailBoxView>(
                    b => (b.LanguageID == request.LanguageId && b.WebsiteID == request.WebsiteID) &&
                        (string.IsNullOrEmpty(request.Keyword) || b.ToEmail.Contains(request.Keyword) ||
                                        b.Subject.Contains(request.Keyword)),
                    q => q.OrderByDescending(b => b.DateCreated),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
