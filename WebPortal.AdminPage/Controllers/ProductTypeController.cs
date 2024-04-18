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
    public class ProductTypeController : BaseController
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;
        public ProductTypeController(IProductTypeService productTypeService,
            IMapper mapper,
            IWebsiteService websiteService) : base(websiteService)
        {
            _mapper = mapper;
            _productTypeService = productTypeService;
        }
        public async Task<IActionResult> Index([FromQuery]ProductTypeSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            var result = await _productTypeService.GetPaging(request);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypeRequest request)
        {
            if (ModelState.IsValid)
            {
                request.LanguageID = LanguageID;
                request.WebsiteID = WebsiteID;

                await _productTypeService.Create(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var productType = await _productTypeService.GetById(id);
            var productTypeRequest = _mapper.Map<ProductTypeRequest>(productType);
            
            return View(productTypeRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypeRequest request)
        {
            if (ModelState.IsValid)
            {
                await _productTypeService.Update(id, request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productTypeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
