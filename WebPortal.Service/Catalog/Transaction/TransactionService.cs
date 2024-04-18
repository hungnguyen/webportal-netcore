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
using Microsoft.Extensions.DependencyInjection;

namespace WebPortal.Services
{
    public class TransactionService : Service<Transaction, TransactionRequest>, ITransactionService
    {
        public TransactionService(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper) : base(serviceScopeFactory, mapper)
        {
           
        }

        public async Task<PagedResult<TransactionView>> GetPaging(TransactionSearchRequest request)
            => await Find<TransactionView>(
                    b => (request.OrderID == null || b.OrderID == request.OrderID) &&
                        (request.FromDate == null || (b.DateCreated != null && b.DateCreated.Value.Date >= request.FromDate.Value.Date)) &&
                        (request.ToDate == null || (b.DateCreated != null && b.DateCreated.Value.Date <= request.ToDate.Value.Date)),
                    q => q.OrderByDescending(b => b.DateCreated),
                    pageIndex: request.PageIndex, pageSize: request.PageSize
                );
        
    }
}
