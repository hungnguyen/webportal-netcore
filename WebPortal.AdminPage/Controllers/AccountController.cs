using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService appUserService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IStorageService storageService;
        private readonly IEmailService emailService;
        private readonly IWebsiteService websiteService;
        private readonly SignInManager<AppUser> signInManager;
        public AccountController(IAppUserService appUserService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IStorageService storageService,
            IWebsiteService websiteService,
            IEmailService emailService,
            SignInManager<AppUser> signInManager)
        {            
            this.appUserService = appUserService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.storageService = storageService;
            this.emailService = emailService;
            this.websiteService = websiteService;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await appUserService.GetByName(User.Identity.Name);
                if (user != null)
                {
                    var userRequest = mapper.Map<AppUserRequest>(user);
                    userRequest.IsOnline = false;
                    await appUserService.UpdateById(user.Id, userRequest);
                }

                await appUserService.SignOut();
            }
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            if (!ModelState.IsValid)
                return View();
                

            //var user = await appUserService.GetByName(request.UserName);

            //if (user != null)
            //{
            //    var claims = new List<Claim>
            //    {
            //        new Claim("Image", user.Image ?? "noavatar.png"),
            //        new Claim("FullName", user.FullName ?? user.UserName)
            //    };
            //    await appUserService.UpsertClaim(user, claims);
            //}

            //var identityResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);
            //if (identityResult.Succeeded)
            //{
            //    if (user != null)
            //    {
            //        HttpContext.Session.SetString("UserImage", storageService.GetFileUrl(user.Image));

            //        var userRequest = mapper.Map<AppUserRequest>(user);
            //        userRequest.LastLoginDate = DateTime.Now;
            //        userRequest.IsOnline = true;
            //        userRequest.IP = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //        userRequest.Browser = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
            //        await appUserService.UpdateById(user.Id, userRequest);
            //    }
            //    return RedirectToAction("Index", "Home");
            //}
            //else if (identityResult.IsLockedOut)
            //{
            //    ModelState.AddModelError("", "User account locked out.");
            //}
            //else if (identityResult.IsNotAllowed)
            //{
            //    ModelState.AddModelError("", "User account is not allowed.");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Username or password is incorrect.");
            //}

            var appResult = await appUserService.SignIn(request);
            if (appResult.Result.Succeeded)
            {
                var user = appResult.ReturnObj;
                if (user != null)
                {
                    var userRequest = mapper.Map<AppUserRequest>(user);
                    userRequest.LastLoginDate = DateTime.Now;
                    userRequest.IsOnline = true;
                    userRequest.IP = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    userRequest.Browser = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                    await appUserService.UpdateById(user.Id, userRequest);
                }
                return RedirectToAction("Index", "Home");
            }
            else if (appResult.Result.IsLockedOut)
            {
                ModelState.AddModelError("", "User account locked out.");
            }
            else if (appResult.Result.IsNotAllowed)
            {
                ModelState.AddModelError("", "User account is not allowed.");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is incorrect.");
            }
            return View();
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            var user = await appUserService.GetByName(User.Identity.Name);
            if (user != null)
            {
                var userRequest = mapper.Map<AppUserRequest>(user);
                userRequest.IsOnline = false;
                await appUserService.UpdateById(user.Id, userRequest);
            }

            await appUserService.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await appUserService.GetByName(User.Identity.Name);
            var userRequest = mapper.Map<AppUserRequest>(user);

            return View(userRequest);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(AppUserRequest request)
        {
            if(ModelState.IsValid)
            {
                if (request.NewImage != null)
                {
                    request.Image = await storageService.SaveFileAsync(request.NewImage);
                }
                
                var appResult = await appUserService.UpdateByName(User.Identity.Name, request);
                if (!appResult.Result.Succeeded)
                {
                    foreach (var e in appResult.Result.Errors)
                    {
                        ModelState.AddModelError(e.Code, e.Description);
                    }
                    return View(request);
                }
                else
                {
                    return RedirectToAction("UpdateSucceed");
                }    
            }    
            
            return View(request);
        }
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                
                var user = await appUserService.GetByName(User.Identity.Name);
                
                var isCorrect = await appUserService.CheckPassword(user, request.CurrentPassword);
                if (isCorrect)
                {
                    var result = await appUserService.ChangePassword(user, request.NewPassword);
                    
                    if (!result.Succeeded)
                    {
                        foreach (var e in result.Errors)
                        {
                            ModelState.AddModelError(e.Code, e.Description);
                        }
                    }
                    return RedirectToAction("UpdateSucceed");
                }
                else
                    ModelState.AddModelError("", "Password is incorrect.");
            }
            return View();
        }
        [Authorize]
        public IActionResult UpdateSucceed()
        {
            return View();
        }
        //private async Task RemoveAllClaims(AppUser user, List<Claim> claims)
        //{
        //    foreach (var c in claims)
        //    {
        //        while (true)
        //        {
        //            var userClaims = await _userManager.GetClaimsAsync(user);
        //            var claim = userClaims.FirstOrDefault(uc => uc.Type == c.Type);
        //            if (claim != null)
        //                await _userManager.RemoveClaimAsync(user, claim);
        //            else
        //                break;
        //        }
                
        //    }
        //}
        //private async Task UpsertClaim(AppUser user, List<Claim> claims)
        //{
        //    foreach (var c in claims)
        //    {
        //        var userClaims = await _userManager.GetClaimsAsync(user);
        //        var claim = userClaims.FirstOrDefault(uc => uc.Type == c.Type);
        //        if (claim != null)
        //            await _userManager.ReplaceClaimAsync(user, claim, c);
        //        else
        //            await _userManager.AddClaimAsync(user, c);
        //    }
        //}
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await appUserService.GetByEmail(request.Email);
                if (user != null)
                {
                    var token = await appUserService.GenerateResetPasswordToken(user);
                    var callback = Url.Action(nameof(ResetPassword), nameof(AccountController).Replace("Controller", ""), new { token, email = user.Email }, Request.Scheme);

                    var domain = Request.Host.Host;
                    var website = await websiteService.GetWebsiteByDomain(domain);
                    emailService.Send(website.ProjectName, website.FromEmail, user.Email, "Reset password token", callback,
                        website.SMTPServer, website.SMTPServerPort, website.SMTPUserName, website.SMTPUserPassword, website.SMTPSSL);
                    
                    ViewBag.IsOk = true;
                }
                else
                {
                    ModelState.AddModelError("", "Can't find user account with your email.");
                }
            }
            return View();
        }
        public IActionResult ResetPassword(string token, string email)
        {
            var request = new ResetPasswordRequest() { Token = token, Email = email };
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await appUserService.GetByEmail(request.Email);
                if (user != null)
                {
                    var result = await appUserService.ResetPassword(user, request.Token, request.NewPassword);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.TryAddModelError(error.Code, error.Description);
                        }
                        return View(request);
                    }
                    else
                    {
                        ViewBag.IsOk = true;
                    }    
                }
                else
                {
                    ModelState.AddModelError("", "Can't find user account with your email.");
                }
            }
            return View(request);
        }
    }
}
