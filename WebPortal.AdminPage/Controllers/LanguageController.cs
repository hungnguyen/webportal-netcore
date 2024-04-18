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
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        public LanguageController(ILanguageService languageService,
            IMapper mapper,
            IWebsiteService websiteService) : base(websiteService)
        {
            _languageService = languageService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index([FromQuery]LanguageSearchRequest request)
        {
            request.WebsiteID = WebsiteID;

            var languages = await _languageService.GetPaging(request);
            return View(languages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LanguageRequest request)
        {
            if (ModelState.IsValid)
            {
                request.WebsiteID = WebsiteID;

                await _languageService.Create(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var language = await _languageService.GetById(id);
            if(language==null)
            {
                return RedirectToAction("Index");
            }

            var languageRequest = new LanguageRequest();
            _mapper.Map(language, languageRequest);

            return View(languageRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LanguageRequest request)
        {
            if (ModelState.IsValid)
            {
                await _languageService.Update(id, request);
                return RedirectToAction("Index");
            }

            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _languageService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
