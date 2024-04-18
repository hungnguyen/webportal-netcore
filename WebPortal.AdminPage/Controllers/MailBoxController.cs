using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Services;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class MailBoxController : BaseController
    {
        private readonly IMailBoxService mailBoxService;
        public MailBoxController(IMailBoxService mailBoxService, 
            IWebsiteService websiteService) : base(websiteService)
        {
            this.mailBoxService = mailBoxService;
        }
        public async Task<IActionResult> Index([FromQuery] MailBoxSearchRequest request)
        {
            request.LanguageId = LanguageID;
            request.WebsiteID = WebsiteID;
            ViewBag.SearchRequest = request;

            var result = await mailBoxService.GetPaging(request);
            return View(result);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var mailBox = await mailBoxService.GetById(id);
            return View(mailBox);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await mailBoxService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
