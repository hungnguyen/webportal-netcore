using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Services;

namespace WebPortal.AdminPage.Controllers.Components
{
    public class ProductMenuViewComponent : ViewComponent
    {
        private readonly IProductTypeService _productTypeService;
        public ProductMenuViewComponent(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var productTypes = await _productTypeService.GetAll();
            var pts = productTypes.Where(pt => pt.Status == Data.Enums.Status.Active).ToList();
            return View("Default", pts);
        }
    }
}
