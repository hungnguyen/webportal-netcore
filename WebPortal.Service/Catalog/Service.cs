using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public abstract class Service<TEntity, TRequest> : IService<TEntity, TRequest>
        where TEntity : class
        where TRequest : class
    {
        protected readonly IServiceScopeFactory serviceScopeFactory;
        protected readonly IMapper mapper;
        public Service(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.mapper = mapper;
        }
        public async Task<TEntity> Create(TRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();

                var entity = mapper.Map<TEntity>(request);
                dbContext.Set<TEntity>().Add(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<TEntity> Delete(object id)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();

                var entity = await dbContext.Set<TEntity>().FindAsync(id);
                if (entity != null)
                {
                    dbContext.Set<TEntity>().Remove(entity);
                    await dbContext.SaveChangesAsync();
                }
                return entity;
            }
        }

        public async Task<PagedResult<TView>> Find<TView>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();

                IQueryable<TEntity> query = dbContext.Set<TEntity>();
                //filter data
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                //add relate data
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                //add order
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                //get total record
                var total = await query.CountAsync();
                if (total > pageSize && pageSize > 0)
                {
                    query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }

                var data = await mapper.ProjectTo<TView>(query)
                        .ToListAsync();
                //var data = await query.ToListAsync();

                var result = new PagedResult<TView>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalRow = total,
                    Items = data
                };
                return result;
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                return await dbContext.Set<TEntity>().ToListAsync();
            }
        }

        public async Task<TEntity> GetById(object id)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                return await dbContext.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<TEntity> Update(object id, TRequest request)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                var entity = await dbContext.Set<TEntity>().FindAsync(id);
                if (entity != null)
                {
                    mapper.Map(request, entity);
                    dbContext.Entry(entity).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                }
                return entity;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebPortalDbContext>();
                dbContext.Entry(entity).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                return entity;
            }
        }
    }
}
