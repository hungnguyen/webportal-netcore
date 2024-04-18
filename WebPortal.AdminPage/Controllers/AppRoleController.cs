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
    public class AppRoleController : Controller
    {
        private readonly IAppRoleService appRoleService;
        private readonly IMapper mapper;
        public AppRoleController(IAppRoleService appRoleService,
            IMapper mapper)
        {
            this.appRoleService = appRoleService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index([FromQuery]AppRoleSearchRequest request)
        {
            ViewBag.SearchRequest = request;

            var result = await appRoleService.GetPaging(request);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AppRoleRequest request)
        {
            if (ModelState.IsValid)
            {
                request.NormalizedName = request.Name.ToLower();
                await appRoleService.Create(request);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var appRole = await appRoleService.GetById(id);
            var appRoleRequest = mapper.Map<AppRoleRequest>(appRole);
            return View(appRoleRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AppRoleRequest request)
        {
            if (ModelState.IsValid)
            {
                request.NormalizedName = request.Name.ToLower();
                await appRoleService.Update(id,request);
                return RedirectToAction("Index");
            }
            return View(request);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await appRoleService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
