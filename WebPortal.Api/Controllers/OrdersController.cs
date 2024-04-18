using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController<Order,OrderRequest,IOrderService>
    {
        private readonly WebPortalDbContext dbContext;
        public OrdersController(IOrderService orderService, WebPortalDbContext dbContext) : base(orderService)
        {
            this.dbContext = dbContext;
        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<OrderView>>> GetPaging([FromQuery] OrderSearchRequest request)
        {
            return await service.GetPaging(request);
        }
        [HttpGet]
        [Route("GetDetailById")]
        public async Task<ActionResult<Order>> GetDetailById(int id)
        {
            return await dbContext.Orders.Include(o => o.Customer).Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.ID == id);
        }
        [HttpGet]
        [Route("GetCount")]
        public async Task<ActionResult<int>> GetCount([FromQuery] OrderSearchRequest request)
        {
            return await service.Count(request);
        }
    }
}
