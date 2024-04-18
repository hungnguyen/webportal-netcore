using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.Services
{
    public class AppRoleService : Service<AppRole, AppRoleRequest>, IAppRoleService
    {
        public AppRoleService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }
        public async Task<PagedResult<AppRoleView>> GetPaging(AppRoleSearchRequest request)
            => await Find<AppRoleView>(
                x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword),
                x => x.OrderBy(y => y.Name),
                pageIndex: request.PageIndex, pageSize: request.PageSize);
    }
}
