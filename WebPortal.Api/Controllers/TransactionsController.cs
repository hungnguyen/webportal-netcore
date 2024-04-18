using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : BaseController<Transaction, TransactionRequest, ITransactionService>
    {
        public TransactionsController(ITransactionService transactionService) : base(transactionService)
        {

        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<TransactionView>>> GetPaging([FromQuery] TransactionSearchRequest request)
        {
            return await service.GetPaging(request);
        }
    }
}
