using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IPageViewService : IService<PageView, PageViewRequest>
    {
        Task<PagedResult<PageViewView>> GetPaging(PageViewSearchRequest request);
        Task<int> Increase(int websiteId);
    }
}
