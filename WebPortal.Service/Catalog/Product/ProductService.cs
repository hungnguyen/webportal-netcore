using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Utilities.Exeptions;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;
using System.Linq;
using WebPortal.Services.Common;
using WebPortal.Utilities.Constants;
using Microsoft.Extensions.DependencyInjection;
using WebPortal.Data.Enums;

namespace WebPortal.Services
{
    public class ProductService : Service<Product, ProductRequest>, IProductService
    {
        public ProductService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {

        }

        public async Task<int> Count(ProductSearchRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from b in dbContext.Products
                            where b.WebsiteID == request.WebsiteID && b.LanguageID == request.LanguageId && b.TypeCode == request.TypeCode
                            select b;

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.Name.Contains(request.Keyword));
                }
                if (request.IsHot != null)
                {
                    query = query.Where(x => x.IsHot == request.IsHot);
                }
                if (request.IsFeature != null)
                {
                    query = query.Where(x => x.IsFeature == request.IsFeature);
                }
                if (request.Status != null)
                {
                    query = query.Where(x => x.Status == request.Status);
                }

                return await query.CountAsync();
            }
        }

        public async Task<Product> GetByUrlName(string urlName)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                return await dbContext.Products.Where(b => b.UrlName.Equals(urlName)).FirstOrDefaultAsync();
            }
        }

        public async Task<PagedResult<ProductView>> GetPaging(ProductSearchRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from b in dbContext.Products
                            where b.WebsiteID == request.WebsiteID && b.LanguageID == request.LanguageId
                            select b;

                if (request.CategoryID != 0)
                {
                    var proid = from o in dbContext.ProductInCategories
                                where o.CategoryID == request.CategoryID
                                select o.ProductID;

                    query = query.Where(b => proid.Contains(b.ID));
                }

                if (!string.IsNullOrEmpty(request.TypeCode))
                {
                    query = query.Where(x => x.TypeCode == request.TypeCode);
                }
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.Name.Contains(request.Keyword) || x.Text3.Contains(request.Keyword));
                }
                if (request.IsHot != null)
                {
                    query = query.Where(x => x.IsHot == request.IsHot);
                }
                if (request.IsFeature != null)
                {
                    query = query.Where(x => x.IsFeature == request.IsFeature);
                }
                if (request.Status != null)
                {
                    query = query.Where(x => x.Status == request.Status);
                }
                if (request.Ids != null && request.Ids.Count > 0)
                {
                    query = query.Where(x => request.Ids.Contains(x.ID));
                }
                switch (request.Order)
                {
                    case ProductOrder.NameAsc:
                        query = query.OrderBy(x => x.Name);
                        break;
                    case ProductOrder.NameDesc:
                        query = query.OrderByDescending(x => x.Name);
                        break;
                    case ProductOrder.ViewAsc:
                        query = query.OrderBy(x => x.ViewCount);
                        break;
                    case ProductOrder.ViewDesc:
                        query = query.OrderByDescending(x => x.ViewCount);
                        break;
                    case ProductOrder.PriceAsc:
                        query = query.OrderBy(x => x.Text5);
                        break;
                    case ProductOrder.PriceDesc:
                        query = query.OrderByDescending(x => x.Text5);
                        break;
                    case ProductOrder.DefaultOrder:
                    default:
                        query = query.OrderByDescending(x => x.DateUpdated);
                        break;
                }


                var total = await query.CountAsync();

                if (total > request.PageSize && request.PageSize > 0)
                {
                    query = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
                }

                var data = await mapper.ProjectTo<ProductView>(query)
                        .ToListAsync();

                var result = new PagedResult<ProductView>()
                {
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalRow = total,
                    Items = data
                };
                return result;
            }
        }

        public async Task<Category> GetPrimaryCategory(int productId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var product = await GetById(productId);
                var productInCategorie = await dbContext.ProductInCategories
                    .Include(pic => pic.Category)
                    .Where(pic => pic.ProductID == productId && pic.Category.TypeCode == product.TypeCode)
                    .FirstOrDefaultAsync();

                if (productInCategorie != null)
                    return productInCategorie.Category;
                return null;
            }
        }

        public async Task<Product> GetOneByCategoryId(ProductSearchRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from b in dbContext.Products
                            where b.WebsiteID == request.WebsiteID && b.LanguageID == request.LanguageId && b.TypeCode == request.TypeCode
                            select b;

                if (request.CategoryID != 0)
                {
                    var proid = from o in dbContext.ProductInCategories
                                where o.CategoryID == request.CategoryID
                                select o.ProductID;

                    query = query.Where(b => proid.Contains(b.ID));
                }
                if (request.Status != null)
                {
                    query = query.Where(x => x.Status == request.Status);
                }

                var data = await query.FirstOrDefaultAsync();

                return data;
            }
        }
    }
}
