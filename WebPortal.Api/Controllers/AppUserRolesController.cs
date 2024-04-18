using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Services;

namespace WebPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserRolesController : AuthorizeController
    {
        private readonly IAppUserService appUserService;
        public AppUserRolesController(IAppUserService appUserService)
        {
            this.appUserService = appUserService;
        }
        [HttpGet]
        [Route("GetByUserId")]
        public async Task<ActionResult<List<string>>> GetByUserId(Guid id)
        {
            var user = await appUserService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return  Ok(await appUserService.GetRoles(user));
        }
        [HttpPost]
        [Route("RoleAssign")]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            var user = await appUserService.GetById(request.id);
            if (user == null)
            {
                return NotFound();
            }

            if (request.inrole.Count > 0)
            {
                var result = await appUserService.RoleAssign(user, request.inrole);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }
            
            return Ok();
        }
    }
    public class RoleAssignRequest
    {
        public Guid id { get; set; }
        public List<string> inrole { get; set; }
    }
}
