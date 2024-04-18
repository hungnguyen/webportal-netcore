using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.Services.Converters;
using WebPortal.Services.Extensions;
using WebPortal.Utilities.Constants;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class PhraseController : BaseController
    {
        private readonly IPhraseService _phraseService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache memoryCache;
        private readonly IToolService toolService;
        public PhraseController(IPhraseService phraseService,
            IMapper mapper,
            IWebsiteService websiteService,
            IToolService toolService,
            IMemoryCache memoryCache) : base(websiteService)
        {
            this.memoryCache = memoryCache;
            this.toolService = toolService;
            _mapper = mapper;
            _phraseService = phraseService;
        }
        public async Task<IActionResult> Index([FromQuery]PhraseSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            ViewBag.SearchRequest = request;

            var phrases = await _phraseService.GetPaging(request);
            return View(phrases);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string RemoveHtml, PhraseRequest request)
        {
            if(ModelState.IsValid)
            {
                request.LanguageID = LanguageID;
                request.WebsiteID = WebsiteID;
                if (TConverter.ChangeType<bool>(RemoveHtml))
                {
                    request.Value = request.Value.RemoveHtml();
                }
                await _phraseService.Create(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var phrase = await _phraseService.GetById(id);
            if (phrase == null) return RedirectToAction("Index");

            var phraseRequest = new PhraseRequest();
            _mapper.Map(phrase, phraseRequest);

            return View(phraseRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string RemoveHtml, PhraseRequest request)
        {
            if (ModelState.IsValid)
            {
                memoryCache.Remove(SystemConstant.CachePhrase);
                if(TConverter.ChangeType<bool>(RemoveHtml))
                {
                    request.Value = request.Value.RemoveHtml();
                }    
                await _phraseService.Update(id, request);
                return RedirectToAction("Index");
            }
            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _phraseService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
