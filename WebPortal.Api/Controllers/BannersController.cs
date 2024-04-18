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
    public class BannersController : BaseController<Banner,BannerRequest,IBannerService>
    {
        private readonly IStorageService _storageService;
        public BannersController(IBannerService bannerService, IStorageService storageService) : base(bannerService)
        {
            _storageService = storageService;
        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<BannerView>>> GetPaging([FromQuery] BannerSearchRequest request)
        {
            return await service.GetPaging(request);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public override async Task<ActionResult<Banner>> Delete(int id)
        {
            var entity = await service.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _storageService.DeleteFileAsync(entity.Image);
            return Ok();
        }
    }
}
