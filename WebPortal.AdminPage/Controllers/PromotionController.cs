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
    public class PromotionController : BaseController
    {
        private readonly IPromotionService promotionService;
        private readonly IMapper mapper;
        public PromotionController(IPromotionService promotionService,
            IMapper mapper,
            IWebsiteService websiteService) : base(websiteService)
        {
            this.promotionService = promotionService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index([FromQuery]PromotionSearchRequest request)
        {
            request.LanguageId = LanguageID;
            request.WebsiteID = WebsiteID;

            ViewBag.SearchRequest = request;

            var result = await promotionService.GetPaging(request);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PromotionRequest request)
        {
            if (ModelState.IsValid)
            {
                await promotionService.Create(request);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var promotion = await promotionService.GetById(id);
            var promotionReq = mapper.Map<PromotionRequest>(promotion);
            return View(promotionReq);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PromotionRequest request)
        {
            if (ModelState.IsValid)
            {
                await promotionService.Update(id, request);
                return RedirectToAction("Index");
            }
            return View(request);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await promotionService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
