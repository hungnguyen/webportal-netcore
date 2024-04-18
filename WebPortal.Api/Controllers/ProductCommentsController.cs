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
    public class ProductCommentsController : BaseController<ProductComment, ProductCommentRequest, IProductCommentService>
    {
        public ProductCommentsController(IProductCommentService productCommentService) : base(productCommentService)
        {

        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<ProductCommentView>>> GetPaging([FromQuery] ProductCommentSearchRequest request)
        {
            return await service.GetPaging(request);
        }
    }
}
