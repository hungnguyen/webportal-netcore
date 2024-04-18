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
    public class WebsiteService : Service<Website, WebsiteRequest>, IWebsiteService
    {
        public WebsiteService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<Website> GetWebsiteByDomain(string domainName)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                return await dbContext.Websites.Where(w => w.Domain.Contains(domainName) || w.DomainAlias.Contains(domainName)).FirstOrDefaultAsync();
            }
        }
    }
}
