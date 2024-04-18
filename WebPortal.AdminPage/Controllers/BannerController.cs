using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class BannerController : BaseController
    {
        private readonly IBannerService _service;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly ICategoryService _categoryService;
        public BannerController(IBannerService service, 
            IMapper mapper, 
            IStorageService storageService,
            ICategoryService categoryService,
            IWebsiteService websiteService) : base(websiteService)
        {
            _service = service;
            _mapper = mapper;
            _storageService = storageService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index([FromQuery]BannerSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            ViewBag.SearchRequest = request;
            ViewBag.ListCat = await _categoryService.GetAll();
            var result = await _service.GetPaging(request);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerRequest request)
        {
            if(ModelState.IsValid)
            {
                request.DateCreated = request.DateUpdated = DateTime.Now;
                request.CreatedBy = request.UpdatedBy = User.Identity.Name;
                request.WebsiteID = WebsiteID;
                request.LanguageID = LanguageID;

                if (request.NewImage != null)
                {
                    request.Image = await _storageService.SaveFileAsync(request.NewImage);
                }

                await _service.Create(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var banner = await _service.GetById(id);
            if(banner==null)
            {
                return RedirectToAction("Index");
            }

            var bannerRequest = new BannerRequest();
            _mapper.Map(banner, bannerRequest);
                        
            bannerRequest.ImageUrl = _storageService.GetFileUrl(bannerRequest.Image);

            return View(bannerRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BannerRequest request)
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

                await _service.Update(id, request);
                return RedirectToAction("Index");
            }
            return View(request);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _service.Delete(id);
            await _storageService.DeleteFileAsync(banner.Image);
            return RedirectToAction("Index");
        }
    }
}
