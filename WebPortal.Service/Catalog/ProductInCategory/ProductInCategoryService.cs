using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Utilities.Exeptions;

namespace WebPortal.Services
{
    public class ProductInCategoryService : IProductInCategoryService
    {
        private readonly WebPortalDbContext _context;

        public ProductInCategoryService(WebPortalDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int productId, List<int> categoryIds)
        {
            List<ProductInCategory> listPIC = new List<ProductInCategory>();
            foreach(var id in categoryIds)
            {
                listPIC.Add(new ProductInCategory()
                {
                    ProductID = productId,
                    CategoryID = id
                });
            }
            _context.AddRange(listPIC);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteByCategoryId(int categoryId)
        {
            var listPIC = await _context.ProductInCategories.Where(x => x.CategoryID == categoryId).ToListAsync();
            _context.ProductInCategories.RemoveRange(listPIC);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteByProductId(int productId)
        {
            var listPIC = await _context.ProductInCategories.Where(x=>x.ProductID == productId).ToListAsync();
            _context.ProductInCategories.RemoveRange(listPIC);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductInCategory>> GetByProductId(int productId)
        {
            return await _context.ProductInCategories.Where(x => x.ProductID == productId).ToListAsync();
        }
    }
}
