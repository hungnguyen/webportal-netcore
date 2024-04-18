using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.ViewModels;

namespace WebPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AccountController : AuthorizeController
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
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    return BadRequest(new { status = 400, error = "User account locked out." });
                }
                else if (result.IsNotAllowed)
                {
                    return BadRequest(new { status = 400, error = "User account is not allowed." });
                }
                else
                {
                    return BadRequest(new { status = 400, error = "Username or password is incorrect." });
                }
            }

            var user = await appUserService.GetByName(model.UserName);

            if (user != null)
            {
                var userRequest = mapper.Map<AppUserRequest>(user);
                userRequest.LastLoginDate = DateTime.Now;
                userRequest.IsOnline = true;
                userRequest.IP = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                userRequest.Browser = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                await appUserService.UpdateById(user.Id, userRequest);
            }

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName)
            }, "Cookies");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync("Cookies", claimsPrincipal);

            return Ok(new { status = 200 });
        }
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var user = await appUserService.GetByName(User.Identity.Name);
            if (user != null)
            {
                var userRequest = mapper.Map<AppUserRequest>(user);
                userRequest.IsOnline = false;
                await appUserService.UpdateById(user.Id, userRequest);
            }

            await Request.HttpContext.SignOutAsync("Cookies");
            return Ok();
        }
        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            if (string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                return NoContent();
            }
            var user = await appUserService.GetByName(User.Identity.Name);

            return Ok(user);
        }
        [HttpGet]
        [Route("CheckLogin")]
        public IActionResult CheckLogin()
        {
            return Ok();
        }
        [HttpGet]
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return Forbid();
        }
    }
}
