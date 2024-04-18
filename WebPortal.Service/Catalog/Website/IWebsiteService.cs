using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IWebsiteService : IService<Website, WebsiteRequest>
    {
        Task<Website> GetWebsiteByDomain(string domainName);
    }
}
