using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebPortal.ViewModels.Common;
using WebPortal.ViewModels;
using WebPortal.Data.Entities;

namespace WebPortal.Services
{
    public interface ITransactionService : IService<Transaction, TransactionRequest>
    {
        Task<PagedResult<TransactionView>> GetPaging(TransactionSearchRequest request);
    }
}
