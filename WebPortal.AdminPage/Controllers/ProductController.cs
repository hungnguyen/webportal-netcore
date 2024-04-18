using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.Services.Extensions;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.AdminPage.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly ICategoryService _categoryService;
        private readonly IToolService _toolService;
        private readonly IMapper _mapper;
        private readonly IProductInCategoryService _productInCategoryService;
        private readonly IStorageService _storageService;
        
        public ProductController(IProductService productService,
            IProductTypeService productTypeService,
            ICategoryService categoryService,
            IToolService toolService,
            IMapper mapper,
            IProductInCategoryService productInCategoryService,
            IStorageService storageService,
            IWebsiteService websiteService) : base(websiteService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _categoryService = categoryService;
            _mapper = mapper;
            _toolService = toolService;
            _productInCategoryService = productInCategoryService;
            _storageService = storageService;
        }
        public async Task<IActionResult> Index([FromQuery]ProductSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            var productType = await _productTypeService.GetByCode(request.TypeCode);
            ViewBag.TypeName = productType != null ? productType.Name : "";
            ViewBag.TypeCode = request.TypeCode;

            List<string> typeCode = new List<string>() { request.TypeCode };
            if (request.TypeCode == "PRD") typeCode.Add("FIL");

            ViewBag.ListCat = CategoryTree(await _categoryService.GetAll(),0,"", typeCode);

            ViewBag.SearchRequest = request;

            var result = await _productService.GetPaging(request);
            return View(result);
        }

        public async Task<IActionResult> Create(string typeCode)
        {
            await BindData(typeCode);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductRequest request)
        {
            if(ModelState.IsValid)
            {
                request.LanguageID = LanguageID;
                request.WebsiteID = WebsiteID;
                request.DateCreated = request.DateUpdated = DateTime.Now;
                request.CreatedBy = request.UpdatedBy = User.Identity.Name;
                if (string.IsNullOrEmpty(request.UrlName))
                    request.UrlName = request.Name.GetUrlName();

                if (request.NewImage != null)
                {
                    request.Image = await _storageService.SaveFileAsync(request.NewImage);
                }

                var product = await _productService.Create(request);

                if (!string.IsNullOrEmpty(request.InCategories))
                {
                    List<int> catIds = request.InCategories.Split(',')
                                    .Select(int.Parse).ToList();

                    await _productInCategoryService.Create(product.ID, catIds);
                }
                

                return RedirectToAction("Index", new { typeCode = request.TypeCode });
            }
            await BindData(request.TypeCode);
            return View(request);
        }

        public async Task<IActionResult> Edit(int id, string typeCode)
        {
            await BindData(typeCode);

            var product = await _productService.GetById(id);
            var productRequest = _mapper.Map<ProductRequest>(product);

            productRequest.ImageUrl = _storageService.GetFileUrl(productRequest.Image);

            var productInCats = await _productInCategoryService.GetByProductId(id);
            var catIds = productInCats.Select(c => c.CategoryID).ToList();

            productRequest.InCategories = string.Join(",", catIds);

            return View(productRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductRequest request)
        {
            if (ModelState.IsValid)
            {
                request.DateUpdated = DateTime.Now;
                request.UpdatedBy = User.Identity.Name;

                if (string.IsNullOrEmpty(request.UrlName))
                    request.UrlName = request.Name.GetUrlName();

                if (request.NewImage != null)
                {
                    await _storageService.DeleteFileAsync(request.Image);
                    request.Image = await _storageService.SaveFileAsync(request.NewImage);
                }

                var product = await _productService.Update(id, request);

                await _productInCategoryService.DeleteByProductId(product.ID);

                if (!string.IsNullOrEmpty(request.InCategories))
                {
                    List<int> catIds = request.InCategories.Split(',')
                                        .Select(int.Parse).ToList();

                    await _productInCategoryService.Create(product.ID, catIds);
                }

                return RedirectToAction("Index", new { typeCode = request.TypeCode });
            }
            return View(request);
        }

        public async Task<IActionResult> Delete(string typeCode, int id)
        {
            var product = await _productService.Delete(id);
            await _storageService.DeleteFileAsync(product.Image);
            return RedirectToAction("Index", new { TypeCode = typeCode });
        }
        private async Task BindData(string typeCode)
        {
            ViewBag.ProductType = await _productTypeService.GetByCode(typeCode);
            var listType = await _productTypeService.GetPaging(new ProductTypeSearchRequest() { PageSize = -1, WebsiteID = WebsiteID, LanguageId = LanguageID, Keyword = typeCode });
            if (typeCode == "PRD")
            {
                var filter = await _productTypeService.GetPaging(new ProductTypeSearchRequest() { PageSize = -1, WebsiteID = WebsiteID, LanguageId = LanguageID, Keyword = "FIL" });
                if (filter.TotalRow > 0) listType.Items.AddRange(filter.Items);
            }
            ViewBag.ListType = listType;
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetById(id);
            return View(product);
        }
        public List<Category> CategoryTree(List<Category> categories, int pid, string prefix, List<string> typeCode)
        {
            List<Category> result = new List<Category>();
            List<Category> childs = categories.Where(c => c.ParentID == pid && typeCode.Contains(c.TypeCode)).OrderBy(c=>c.TypeCode).ToList();
            
            foreach (var c in childs)
            {
                c.Name = prefix + c.Name;
                result.Add(c);
                result.AddRange(CategoryTree(categories, c.ID, prefix+ "--", typeCode));
            }
            return result;
        }
        public async Task<IActionResult> Hot(int id)
        {
            var product = await _productService.GetById(id);
            product.IsHot = !product.IsHot;
            product.DateUpdated = DateTime.Now;
            product.UpdatedBy = User.Identity.Name;
            await _productService.Update(product);
            return RedirectToAction("Index", new { TypeCode = product.TypeCode });
        }
        public async Task<IActionResult> Feature(int id)
        {
            var product = await _productService.GetById(id);
            product.IsFeature = !product.IsFeature;
            product.DateUpdated = DateTime.Now;
            product.UpdatedBy = User.Identity.Name;
            await _productService.Update(product);
            return RedirectToAction("Index", new { TypeCode = product.TypeCode });
        }
    }
}
