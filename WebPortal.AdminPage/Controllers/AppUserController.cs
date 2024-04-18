using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.Services.Extensions;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserService appUserService;
        private readonly IAppRoleService appRoleService;
        private readonly IMapper mapper;
        private readonly IStorageService storageService;        
        private readonly IToolService toolService;
        public AppUserController(IAppUserService appUserService,
            IMapper mapper,
            IStorageService storageService,
            IAppRoleService appRoleService,
            IToolService toolService)
        {
            this.appUserService = appUserService;
            this.mapper = mapper;
            this.storageService = storageService;
            this.appRoleService = appRoleService;
            this.toolService = toolService;
        }
        public async Task<IActionResult> Index([FromQuery]AppUserSearchRequest request)
        {
            ViewBag.SearchRequest = request;

            var result = await appUserService.GetPaging(request);
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            await BindList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AppUserRequest request)
        {
            if (ModelState.IsValid)
            {
                if (request.NewImage != null)
                {
                    request.Image = await storageService.SaveFileAsync(request.NewImage);
                }
                var appResult = await appUserService.Create(request);
                var user = appResult.ReturnObj;

                if (appResult.Result.Succeeded)
                {
                    await appUserService.RoleAssign(user, request.InRole);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var e in appResult.Result.Errors)
                    {
                        ModelState.AddModelError(e.Code, e.Description);
                    }
                }    
            }
            await BindList();
            return View(request);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await appUserService.GetById(id);
            var userRequest = mapper.Map<AppUserRequest>(user);
            var inRoles = await appUserService.GetRoles(user);
            userRequest.InRole = inRoles.ToList();

            userRequest.ImageUrl = storageService.GetFileUrl(userRequest.Image);
            await BindList();
            return View(userRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AppUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var errors = new List<IdentityError>();
                if (request.NewImage != null)
                {
                    await storageService.DeleteFileAsync(request.Image);
                    request.Image = await storageService.SaveFileAsync(request.NewImage);
                }

                var appResult = await appUserService.UpdateById(id, request);
                var user = appResult.ReturnObj;

                if (!appResult.Result.Succeeded)
                {
                    errors.AddRange(appResult.Result.Errors);
                    goto ShowError;
                }

                if (!string.IsNullOrEmpty(request.NewPassword))
                {
                    var result = await appUserService.ChangePassword(user, request.NewPassword);
                    if (result.Succeeded)
                    {
                        errors.AddRange(appResult.Result.Errors);
                        goto ShowError;
                    }
                }

                await appUserService.RoleAssign(user, request.InRole);

                return RedirectToAction("Index");

            ShowError:
                if (errors.Count>0)
                {
                    foreach (var e in errors)
                    {
                        ModelState.AddModelError(e.Code, e.Description);
                    }
                }

            }
            await BindList();
            return View(request);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var appResult = await appUserService.Delete(id);
                var user = appResult.ReturnObj;

                await storageService.DeleteFileAsync(user.Image);
            }
            return RedirectToAction("Index");
        }
        private async Task BindList()
        {
            var roles = await appRoleService.GetAll();
            ViewBag.ListRole = roles;            
        }
        public async Task<IActionResult> LockOrUnlock(string id)
        {
            var user = await appUserService.GetById(id);
            if(user.LockoutEnd.CompareToNow()>0)
            {
                await appUserService.Unlock(user);
            }    
            else
            {
                await appUserService.Lock(user);
            }    
            
            return RedirectToAction("Index");
        }
    }
}
