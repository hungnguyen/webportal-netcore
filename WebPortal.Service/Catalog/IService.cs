using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public interface IService<TEntity, TRequest> 
        where TEntity: class
        where TRequest : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Create(TRequest request);
        Task<TEntity> Update(object id, TRequest request);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(object id);        
        Task<TEntity> GetById(object id);
        Task<PagedResult<TView>> Find<TView>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 0, int pageSize = 10);
    }
}
