using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFilesController : BaseController<ProductFile, ProductFileRequest, IProductFileService>
    {
        public ProductFilesController(IProductFileService productFileService) : base(productFileService)
        {

        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<ProductFileView>>> GetPaging([FromQuery] ProductFileSearchRequest request)
        {
            return await service.GetPaging(request);
        }
        [HttpDelete]
        [Route("DeleteByProductId")]
        public async Task<ActionResult<int>> DeleteByProductId(int id)
        {
            return await service.DeleteByProductId(id);
        }
    }
}
