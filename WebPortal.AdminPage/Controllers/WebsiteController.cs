using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Services;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class WebsiteController : BaseController
    {
        private readonly IMapper mapper;
        public WebsiteController(IWebsiteService websiteService,
            IMapper mapper) : base(websiteService)
        {
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var website = await websiteService.GetById(WebsiteID);
            var webisteRequest = mapper.Map<WebsiteRequest>(website);
            return View(webisteRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Index(WebsiteRequest request)
        {
            if(ModelState.IsValid)
            {
                await websiteService.Update(WebsiteID, request);
                ViewBag.IsOk = true;
            }
            return View(request);
        }
    }
}
