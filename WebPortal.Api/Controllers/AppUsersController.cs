using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Api.Controllers;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.ViewModels;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : AuthorizeController
    {
        private readonly IAppUserService appUserService;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IStorageService _storageService;
        public AppUsersController(IAppUserService appUserService,
            UserManager<AppUser> userManager,
            IMapper mapper,
            IStorageService storageService)
        {
            this.appUserService = appUserService;
            this.userManager = userManager;
            this.mapper = mapper;
            _storageService = storageService;
        }
        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get()
        {
            return await appUserService.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> Get(Guid id)
        {
            var entity = await appUserService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AppUser>> Put(Guid id, [FromBody] AppUserRequest request)
        {
            var user = await appUserService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var appResult = await appUserService.UpdateById(id, request);

            if (!appResult.Result.Succeeded)
            {
                return BadRequest(appResult.Result.Errors);
            }

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var result = await appUserService.ChangePassword(user, request.NewPassword);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            return user;
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<AppUser>> Post([FromBody] AppUserRequest request)
        {
            var appResult = await appUserService.Create(request);
            var user = appResult.ReturnObj;

            if (!appResult.Result.Succeeded)
            {
                return BadRequest(appResult.Result.Errors);
            }
            
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> Delete(Guid id)
        {
            var user = await appUserService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await appUserService.GetRoles(user);

            await appUserService.RemoveFromRoles(user, roles);

            await appUserService.Delete(id);

            await _storageService.DeleteFileAsync(user.Image);

            return user;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await appUserService.GenerateAccessToken(request);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
