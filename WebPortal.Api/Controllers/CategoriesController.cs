using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController<Category, CategoryRequest, ICategoryService>
    {
        private readonly IStorageService _storageService;
        public CategoriesController(ICategoryService categoryService, IStorageService storageService) : base(categoryService)
        {
            _storageService = storageService;
        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<CategoryView>>> GetPaging([FromQuery] CategorySearchRequest request)
        {
            return await service.GetPaging(request);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public override async Task<ActionResult<Category>> Delete(int id)
        {
            var entity = await service.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _storageService.DeleteFileAsync(entity.Image);
            await _storageService.DeleteFileAsync(entity.Icon);
            return Ok();
        }
    }
}
