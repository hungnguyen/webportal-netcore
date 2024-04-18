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
    public class CustomersController : BaseController<Customer, CustomerRequest, ICustomerService>
    {
        private readonly IStorageService _storageService;
        public CustomersController(ICustomerService customerService, IStorageService storageService) : base(customerService)
        {
            _storageService = storageService;
        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<CustomerView>>> GetPaging([FromQuery] CustomerSearchRequest request)
        {
            return await service.GetPaging(request);
        }
        [HttpGet]
        [Route("GetCount")]
        public async Task<ActionResult<int>> GetCount([FromQuery] CustomerSearchRequest request)
        {
            return await service.Count(request);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public override async Task<ActionResult<Customer>> Delete(int id)
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
