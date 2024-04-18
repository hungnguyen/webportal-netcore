using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Api.Controllers;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.ViewModels;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRolesController : AuthorizeController
    {
        private readonly IAppRoleService service;
        private readonly IMapper mapper;
        public AppRolesController(IAppRoleService service,
            IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> Get()
        {
            return await service.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppRole>> Get(Guid id)
        {
            var entity = await service.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AppRole>> Put(Guid id, AppRoleRequest request)
        {
            var entity = await service.GetById(id);
            if (id != entity.Id)
            {
                return BadRequest();
            }
            entity = await service.Update(id, request);
            return entity;
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<AppRole>> Post(AppRoleRequest request)
        {
            var entity = await service.Create(request);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppRole>> Delete(Guid id)
        {
            var entity = await service.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
