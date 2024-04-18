using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebPortal.AdminPage.Models;
using WebPortal.Services;
using WebPortal.Services.Common;

namespace WebPortal.AdminPage.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStorageService _storageService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly IProductCommentService productCommentService;
        public HomeController(ILogger<HomeController> logger, IStorageService storageService,
            IWebsiteService websiteService,
            IOrderService orderService,
            IProductService productService,
            ICustomerService customerService,
            IProductCommentService productCommentService) : base(websiteService)
        {
            _logger = logger;
            _storageService = storageService;
            this.productService = productService;
            this.customerService = customerService;
            this.orderService = orderService;
            this.productCommentService = productCommentService;
        }

        public async Task<IActionResult> Index()
        {
            

            var homeModel = new HomeViewModel()
            {
                TotalBooking = await orderService.Count(new ViewModels.OrderSearchRequest() { PageSize = -1, WebsiteID = WebsiteID, LanguageId = LanguageID }),
                TotalCustomer = await customerService.Count(new ViewModels.CustomerSearchRequest() { PageSize = -1, WebsiteID = WebsiteID, LanguageId = LanguageID }),
                TotalNews = await productService.Count(new ViewModels.ProductSearchRequest() { PageSize = -1, WebsiteID = WebsiteID, LanguageId = LanguageID,TypeCode="NWS" }),
                TotalProduct = await productService.Count(new ViewModels.ProductSearchRequest() { PageSize = -1, WebsiteID = WebsiteID, LanguageId = LanguageID,TypeCode="PRD" }),
                ListOrder=await orderService.GetPaging(new ViewModels.OrderSearchRequest() { PageSize=10, WebsiteID = WebsiteID, LanguageId = LanguageID }),
                ListProduct=await productService.GetPaging(new ViewModels.ProductSearchRequest() { PageSize = 10, WebsiteID = WebsiteID, LanguageId = LanguageID, TypeCode = "PRD" })
                //ProductComments=await productCommentService.GetPaging(new ViewModels.ProductCommentSearchRequest() { PageSize = 6, WebsiteID = WebsiteID, LanguageId = LanguageID }),
                //InternalNews= await productService.GetPaging(new ViewModels.ProductSearchRequest() { PageSize = 1, WebsiteID = WebsiteID, LanguageId = LanguageID, TypeCode = "INW" })
        };
            return View(homeModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> CkUpload(IFormFile upload, string CKEditorFuncNum)
        {
            string folder = Url.Content("~/upload/");
            if (upload != null)
            {
                var filename = await _storageService.SaveFileAsync(upload);
                ViewBag.Script = $"var CKEditorFuncNum = {CKEditorFuncNum};window.parent.CKEDITOR.tools.callFunction( CKEditorFuncNum, '{folder}{filename}');";
            }
            return View();
        }
    }
}
