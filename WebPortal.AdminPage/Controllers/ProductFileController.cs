using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class ProductFileController : BaseController
    {
        private readonly IProductFileService _productFileService;
        private readonly IStorageService _storageService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductFileController(IProductFileService productFileService, 
            IStorageService storageService,
            IProductService productService,
            IMapper mapper,
            IWebsiteService websiteService) : base(websiteService)
        {
            _productFileService = productFileService;
            _storageService = storageService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] ProductFileSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            var product = await _productService.GetById(request.ProductID);
            ViewBag.ProductName = product != null ? product.Name : "";
            ViewBag.ProductID = request.ProductID;

            var result = await _productFileService.GetPaging(request);
            return View(result);
        }

        public IActionResult Create(int productid)
        {
            ViewBag.ProductID = productid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFileRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request.NewFile != null)
                {
                    request.FileName = await _storageService.SaveFileAsync(request.NewFile);
                }

                await _productFileService.Create(request);
                return RedirectToAction("Index", new { productid = request.ProductID });
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id, int productid)
        {
            var productFile = await _productFileService.GetById(id);
            if(productFile==null)
            {
                return RedirectToAction("Index", new { productid });
            }
            var productFileRequest = _mapper.Map<ProductFileRequest>(productFile);
            productFileRequest.FileUrl = _storageService.GetFileUrl(productFileRequest.FileName);
            ViewBag.ProductID = productid;

            return View(productFileRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductFileRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request.NewFile != null)
                {
                    await _storageService.DeleteFileAsync(request.FileName);
                    request.FileName = await _storageService.SaveFileAsync(request.NewFile);
                }

                await _productFileService.Update(id, request);
                return RedirectToAction("Index", new { productid = request.ProductID });
            }
            return View(request);
        }

        public async Task<IActionResult> Delete(int id, int productid)
        {
            var productFile = await _productFileService.Delete(id);
            await _storageService.DeleteFileAsync(productFile.FileName);
            return RedirectToAction("Index", new { productid = productid });
        }
    }
}
