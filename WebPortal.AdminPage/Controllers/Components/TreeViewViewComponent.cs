using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.Data.Enums;
using WebPortal.Services;
using WebPortal.ViewModels;

namespace WebPortal.AdminPage.Controllers.Components
{
    public class TreeViewViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public TreeViewViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(List<ProductTypeView> model)
        {
            string strTree = string.Empty;
            List<Category> ListCategory = await _categoryService.GetAll();
            List<Category> ListCatByType;
            strTree += "<ul>";
            foreach (var item in model)
            {
                ListCatByType = ListCategory.Where(c => c.TypeCode == item.Code).OrderBy(c => c.ParentID).ThenBy(c => c.OrderNumber).ToList();

                if (ListCatByType.Count() > 0)
                {
                    strTree += "<li class='folder expanded'>" + item.Name;
                    strTree += AddCategoryItem(ListCategory, ListCatByType, null); ;
                    strTree += "</li>";
                }
            }
            strTree += "</ul>";
            return View("Default", strTree);
        }
        public string AddCategoryItem(IEnumerable<Category> allCat, IEnumerable<Category> listCat, Category cat)
        {
            string strMenu = string.Empty, strClass = string.Empty, strChild = string.Empty;
            int i = 0;
            IEnumerable<Category> listAllChild;
            if (cat == null)
                listAllChild = listCat.Where(c => c.ParentID == 0).ToList();
            else
                listAllChild = allCat.Where(c => c.ParentID == cat.ID).ToList();
            if (listAllChild.Count() > 0)
            {
                //if (cat != null)
                strMenu += "<ul>";
                foreach (Category child in listAllChild)
                {
                    strClass = string.Empty;
                    strChild = AddCategoryItem(allCat, listCat, child);
                    //if (i == 0 && cat == null) strClass = "class='first'";
                    strClass = (strChild != string.Empty ? "expanded" : "");
                    strMenu += "<li class='" + strClass + "' id='" + child.ID + "'><a href='" + Url.Action("Edit", new { id = child.ID }) + "' target='_self'>" + (child.Status==Status.Active ? child.Name : "<font color='#999'>" + child.Name + "</font>") + "</a>";
                    strMenu += strChild;
                    strMenu += "</li>";
                    i++;
                }
                //if (cat != null)
                strMenu += "</ul>";
            }
            return strMenu;

        }
    }
}
