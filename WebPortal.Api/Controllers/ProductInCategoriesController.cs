using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Api.Controllers;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInCategoriesController : AuthorizeController
    {
        private readonly IProductInCategoryService productInCategoryService;
        public ProductInCategoriesController(IProductInCategoryService productInCategoryService)
        {
            this.productInCategoryService = productInCategoryService;
        }
        [HttpGet]
        [Route("GetByProductId")]
        public async Task<ActionResult<List<ProductInCategory>>> GetByProductId(int id)
        {
            return await productInCategoryService.GetByProductId(id);
        }
        [HttpDelete]
        [Route("DeleteByProductId")]
        public async Task<ActionResult<int>> DeleteByProductId(int id)
        {
            return await productInCategoryService.DeleteByProductId(id);
        }
        [HttpDelete]
        [Route("DeleteByCategoryId")]
        public async Task<ActionResult<int>> DeleteByCategoryId(int id)
        {
            return await productInCategoryService.DeleteByCategoryId(id);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create(ProductInCategoryRequest request)
        {
            return await productInCategoryService.Create(request.ProductId, request.CatIds);
        }
    }
    public class ProductInCategoryRequest
    {
        public int ProductId { get; set; }
        public List<int> CatIds { get; set; }
    }
}
