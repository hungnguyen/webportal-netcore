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
using Microsoft.Extensions.DependencyInjection;

namespace WebPortal.Services
{
    public class ProductFileService : Service<ProductFile, ProductFileRequest>, IProductFileService
    {
        private readonly IStorageService _storageService;
        public ProductFileService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            IStorageService storageService) : base(serviceScopeFactory, mapper)
        {
            _storageService = storageService;
        }

        public async Task<int> DeleteByProductId(int productId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var listPF = await dbContext.ProductFiles.Where(x => x.ProductID == productId).ToListAsync();
                foreach(var item in listPF)
                {
                    await _storageService.DeleteFileAsync(item.FileName);
                }
                dbContext.ProductFiles.RemoveRange(listPF);
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ProductFileView>> GetByProductId(int productId)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var query = from b in dbContext.ProductFiles
                            where b.ProductID == productId
                            select b;

                query = query.OrderBy(b => b.ID);

                var data = await mapper.ProjectTo<ProductFileView>(query)
                        .ToListAsync();

                return data;
            }
        }

        public async Task<PagedResult<ProductFileView>> GetPaging(ProductFileSearchRequest request)
            => await Find<ProductFileView>(
                    b => (request.ProductID == null || b.ProductID == request.ProductID) &&
                        (string.IsNullOrEmpty(request.Keyword) || b.Name.Contains(request.Keyword)),
                    q => q.OrderBy(b => b.OrderNumber),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
