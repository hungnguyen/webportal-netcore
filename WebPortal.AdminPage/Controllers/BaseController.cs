using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.AdminPage.Helpers;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Utilities.Constants;

namespace WebPortal.AdminPage.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public int WebsiteID = 1;
        public string LanguageID = "vi-VN";
        protected readonly IWebsiteService websiteService;
        public BaseController(IWebsiteService websiteService)
        {
            this.websiteService = websiteService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //get website id
            var domain = context.HttpContext.Request.Host.Host;
            var website = context.HttpContext.Session.GetObjectFromJson<Website>(SystemConstant.WebsiteAdminSession);
            if (website == null)
            {
                website = websiteService.GetWebsiteByDomain(domain).Result;
            }
            if (website != null)
            {
                WebsiteID = website.ID;
            }
            else
            {
                context.Result = new ContentResult { Content = "404 : Website not found" };
            }

            //get lang id
            var langid = context.HttpContext.Session.GetString(SystemConstant.LanguageAdminSession);
            if (!string.IsNullOrEmpty(langid))
            {
                LanguageID = langid;
            }

            if (!User.Identity.IsAuthenticated)
            {
                context.Result = RedirectToAction("Login", "Account");
            }
            base.OnActionExecuting(context);
        }
    }
}
