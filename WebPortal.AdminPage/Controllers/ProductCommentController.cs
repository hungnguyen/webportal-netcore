using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using WebPortal.Services;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class ProductCommentController : BaseController
    {
        private readonly IProductCommentService productCommentService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductCommentController(IProductCommentService productCommentService,
            IMapper mapper,
            IProductService productService,
            IWebsiteService websiteService) : base(websiteService)
        {
            this.productCommentService = productCommentService;
            this.mapper = mapper;
            this.productService = productService;
        }
        public async Task<IActionResult> Index([FromQuery]ProductCommentSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            var product = await productService.GetById(request.ProductID);
            ViewBag.ProductName = product != null ? product.Name : "";
            ViewBag.ProductID = request.ProductID;

            ViewBag.SearchRequest = request;

            var result = await productCommentService.GetPaging(request);
            return View(result);
        }
        public async Task<IActionResult> Create(int productId, int parentId)
        {
            var parent = await productCommentService.GetById(parentId);
            var productCommentRequest = new ProductCommentRequest() { ParentID = parentId, ProductID = productId, Subject="RE: "+parent.Subject };
            return View(productCommentRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCommentRequest request)
        {
            if (ModelState.IsValid)
            {
                await productCommentService.Create(request);
                return RedirectToAction("Index", new { productid = request.ProductID });
            }
            return View(request);
        }
        public async Task<IActionResult> Edit(int id, int productid)
        {
            var productComment = await productCommentService.GetById(id);            
            return View(productComment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductCommentRequest request)
        {
            if (ModelState.IsValid)
            {
                await productCommentService.Update(id, request);
                return RedirectToAction("Index", new { productid = request.ProductID });
            }
            return View(request);
        }
        public async Task<IActionResult> Delete(int id, int productid)
        {
            await productCommentService.Delete(id);
            return RedirectToAction("Index", new { productid = productid });
        }
    }
}
