using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.Services
{
    public interface IAppRoleService : IService<AppRole, AppRoleRequest>
    {
        Task<PagedResult<AppRoleView>> GetPaging(AppRoleSearchRequest request);
    }
}
