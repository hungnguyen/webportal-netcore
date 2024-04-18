using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Data.Enums;

namespace WebPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : AuthorizeController
    {
        [HttpGet]
        [Route("getstatus")]
        public ActionResult<List<KeyValuePair<string, int>>> GetStatus()
        {
            return GetListFromEnum<Status>();
        }
        [HttpGet]
        [Route("getposition")]
        public ActionResult<List<KeyValuePair<string, int>>> GetPosition()
        {
            return GetListFromEnum<Position>();
        }
        [HttpGet]
        [Route("getgender")]
        public ActionResult<List<KeyValuePair<string, int>>> GetGender()
        {
            return GetListFromEnum<Gender>();
        }
        [HttpGet]
        [Route("getpaymethod")]
        public ActionResult<List<KeyValuePair<string, int>>> GetPayMethod()
        {
            return GetListFromEnum<PayMethod>();
        }
        [HttpGet]
        [Route("getpaystatus")]
        public ActionResult<List<KeyValuePair<string, int>>> GetPayStatus()
        {
            return GetListFromEnum<PayStatus>();
        }
        [HttpGet]
        [Route("getorderstatus")]
        public ActionResult<List<KeyValuePair<string, int>>> GetOrderStatus()
        {
            return GetListFromEnum<OrderStatus>();
        }
        private List<KeyValuePair<string, int>> GetListFromEnum<T>()
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
            }
            return list;
        }
    }
}
