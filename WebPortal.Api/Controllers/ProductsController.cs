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
    public class ProductsController : BaseController<Product, ProductRequest, IProductService>
    {
        private readonly IStorageService _storageService;
        private readonly IProductFileService _productFileService;
        private readonly IProductInCategoryService _productInCategoryService;
        private readonly IProductCommentService _productCommentService;
        private readonly IProductVoteService _productVoteService;
        public ProductsController(IProductService productService, 
            IStorageService storageService, 
            IProductFileService productFileService, 
            IProductInCategoryService productInCategoryService,
            IProductCommentService productCommentService,
            IProductVoteService productVoteService) : base(productService)
        {
            _storageService = storageService;
            _productFileService = productFileService;
            _productInCategoryService = productInCategoryService;
            _productCommentService = productCommentService;
            _productVoteService = productVoteService;
        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<ProductView>>> GetPaging([FromQuery] ProductSearchRequest request)
        {
            return await service.GetPaging(request);
        }

        [HttpGet]
        [Route("GetCount")]
        public async Task<ActionResult<int>> GetCount([FromQuery] ProductSearchRequest request)
        {
            return await service.Count(request);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public override async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await service.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productFileService.DeleteByProductId(id);
            await _productInCategoryService.DeleteByProductId(id);
            await _productCommentService.DeleteByProductId(id);
            await _productVoteService.DeleteByProductId(id);

            await service.Delete(id);
            await _storageService.DeleteFileAsync(product.Image);

            return Ok();
        }
    }
}
