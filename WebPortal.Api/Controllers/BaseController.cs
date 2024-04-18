using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Api.Controllers;
using WebPortal.Data.Entities;
using WebPortal.Services;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TRequest, TService> : AuthorizeController
        where TEntity : class, IEntity
        where TRequest : class
        where TService : IService<TEntity, TRequest>
    {
        protected readonly TService service;
        public BaseController(TService service)
        {
            this.service = service;
        }
        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await service.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(int id)
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
        public virtual async Task<ActionResult<TEntity>> Put(int id, [FromBody]TRequest request)
        {
            var entity = await service.GetById(id);
            if (id != entity.ID)
            {
                return BadRequest();
            }
            entity = await service.Update(id, request);
            return entity;
        }

        // POST: api/[controller]
        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Post([FromBody]TRequest request)
        {
            var entity = await service.Create(request);
            return CreatedAtAction("Get", new { id = entity.ID }, entity);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TEntity>> Delete(int id)
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
