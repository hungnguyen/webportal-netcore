using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.ViewModels;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : BaseController<Website, WebsiteRequest, IWebsiteService>
    {
        public WebsitesController(IWebsiteService websiteService) : base(websiteService)
        {
            
        }
        [HttpGet]
        [Route("GetByDomain")]
        public async Task<ActionResult<Website>> GetByDomain(string domain)
        {
            return await service.GetWebsiteByDomain(domain);
        }
    }
}
