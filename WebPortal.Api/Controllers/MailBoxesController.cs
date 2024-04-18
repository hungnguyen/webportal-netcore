using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
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
    public class MailBoxesController : BaseController<MailBox, MailBoxRequest, IMailBoxService>
    {
        public MailBoxesController(IMailBoxService mailBoxService) : base(mailBoxService)
        {

        }
        // GET: api/[controller]/GetPaging?param1=&param2=....
        [HttpGet]
        [Route("GetPaging")]
        public async Task<ActionResult<PagedResult<MailBoxView>>> GetPaging([FromQuery] MailBoxSearchRequest request)
        {
            return await service.GetPaging(request);
        }
    }
}
