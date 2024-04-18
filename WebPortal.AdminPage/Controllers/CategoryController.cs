using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.Services.Extensions;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductTypeService _productTypeService;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly IToolService _toolService;
        private readonly IProductInCategoryService productInCategoryService;
        public CategoryController(ICategoryService categoryService,
            IMapper mapper,
            IStorageService storageService,
            IProductTypeService productTypeService,
            IToolService toolService,
            IWebsiteService websiteService,
            IProductInCategoryService productInCategoryService) : base(websiteService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _storageService = storageService;
            _productTypeService = productTypeService;
            _toolService = toolService;
            this.productInCategoryService = productInCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _productTypeService.GetPaging(new ProductTypeSearchRequest() { PageSize=-1, LanguageId=LanguageID, WebsiteID=WebsiteID });
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            await LoadSelectItem();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            if (ModelState.IsValid)
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

                await _categoryService.Create(request);
                return RedirectToAction("Index");
            }
            await LoadSelectItem();
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            await LoadSelectItem();

            var category = await _categoryService.GetById(id);
            var categoryRequest = _mapper.Map<CategoryRequest>(category);

            categoryRequest.ImageUrl = _storageService.GetFileUrl(category.Image);
            categoryRequest.IconUrl = _storageService.GetFileUrl(category.Icon);

            return View(categoryRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                request.DateUpdated = DateTime.Now;
                request.UpdatedBy = User.Identity.Name;

                if (request.NewImage != null)
                {
                    await _storageService.DeleteFileAsync(request.Image);
                    request.Image = await _storageService.SaveFileAsync(request.NewImage);
                }
                if (request.NewIcon != null)
                {
                    await _storageService.DeleteFileAsync(request.Icon);
                    request.Icon = await _storageService.SaveFileAsync(request.NewIcon);
                }

                if (string.IsNullOrEmpty(request.UrlName))
                    request.UrlName = request.Name.GetUrlName();

                await _categoryService.Update(id, request);
                return RedirectToAction("Index");
            }
            await LoadSelectItem();

            return View(request);
        }

        public async Task<IActionResult> Delete(string hfIdSelected)
        {
            var listId = hfIdSelected.Split(',').Select(int.Parse).ToList();
            foreach (var i in listId)
            {
                await productInCategoryService.DeleteByCategoryId(i);
                var category = await _categoryService.Delete(i);

                await _storageService.DeleteFileAsync(category.Image);
                await _storageService.DeleteFileAsync(category.Icon);
            }
            
            return RedirectToAction("Index");
        }

        private async Task LoadSelectItem()
        {
            ViewBag.ListCat = await _categoryService.GetAll();
            ViewBag.ListType = await _productTypeService.GetAll();
        }
        public async Task<IActionResult> GetListByType(string typeCode)
        {
            var cats = await _categoryService.GetPaging(new CategorySearchRequest() { WebsiteID = WebsiteID, LanguageId = LanguageID, PageSize = -1, TypeCode = typeCode });

            return Json(cats.Items);
        }
    }
}
