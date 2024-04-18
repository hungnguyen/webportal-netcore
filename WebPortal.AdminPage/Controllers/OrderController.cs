using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Data.Enums;
using WebPortal.Services;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;
        private readonly ICustomerService customerService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IAppUserService appUserService;
        public OrderController(IOrderService orderService,
            IOrderItemService orderItemService,
            ICustomerService customerService,
            IProductService productService,
            IMapper mapper,
            IAppUserService appUserService,
            IWebsiteService websiteService) : base(websiteService)
        {
            this.orderService = orderService;
            this.orderItemService = orderItemService;
            this.customerService = customerService;
            this.productService = productService;
            this.mapper = mapper;
            this.appUserService = appUserService;
        }
        public async Task<IActionResult> Index([FromQuery]OrderSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;

            ViewBag.SearchRequest = request;

            var result = await orderService.GetPaging(request);
            return View(result);
        }
        public async Task<IActionResult> Export([FromQuery] OrderSearchRequest request)
        {
            request.WebsiteID = WebsiteID;
            request.LanguageId = LanguageID;
            request.PageSize = -1;
            var result = await orderService.GetPaging(request);

            var builder = new StringBuilder();
            builder.AppendLine("Id,Customer,Sale,OrderStatus,OrderDate,TotalAmout,PayMethod,PayStatus");
            foreach (var o in result.Items)
            {
                builder.AppendLine($"{o.ID},{o.CustomerName},{o.SaleName},{o.OrderStatus},{o.OrderDate},{o.TotalAmout},{o.PayMethod},{o.PayStatus}");
            }

            var data = Encoding.UTF8.GetBytes(builder.ToString());
            var utf8data = Encoding.UTF8.GetPreamble().Concat(data).ToArray();
            return File(utf8data, "text/csv", "orders.csv");            
        }
        public async Task<IActionResult> Detail(int id, bool updated)
        {
            var order = await orderService.GetById(id);
            var customer = await customerService.GetById(order.CustomerID);
            var orderItems = await orderItemService.GetByOrderId(order.ID);

            var orderView = mapper.Map<OrderView>(order);
            orderView.Customer = customer;
            orderView.OrderItems = orderItems;

            ViewBag.ListSale = await appUserService.GetAll();
            ViewBag.IsOk = updated;
            return View(orderView);
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(int id, Guid saleID, OrderStatus orderStatus, PayStatus payStatus)
        {
            var order = await orderService.GetById(id);
            var orderRequest = mapper.Map<OrderRequest>(order);
            if(saleID!=Guid.Empty)
                orderRequest.SaleID = saleID;
            orderRequest.OrderStatus = orderStatus;
            orderRequest.PayStatus = payStatus;
            orderRequest.DateUpdate = DateTime.Now;
            orderRequest.UpdatedBy = User.Identity.Name;

            await orderService.Update(id, orderRequest);
            
            return RedirectToAction("Detail", new { id = id, updated = true });
        }
    }
}
