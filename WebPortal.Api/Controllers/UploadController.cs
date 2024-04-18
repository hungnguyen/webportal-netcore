using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Services.Common;

namespace WebPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : AuthorizeController
    {
        private readonly IStorageService storageService;
        public UploadController(IStorageService storageService)
        {
            this.storageService = storageService;
        }
        [HttpPost]
        [Route("UploadToCkeditor")]
        public async Task<IActionResult> UploadToCkeditor(IFormFile file)
        {
            try
            {
                string fileName = "", fileUrl = "";

                fileName = await storageService.SaveFileAsync(file);
                fileUrl = storageService.GetAbsoluteUrl(fileName);

                return Ok(new
                {
                    url = fileUrl
                });
            }
            catch (System.Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                string fileName = "";

                if (file != null)
                {
                    fileName = await storageService.SaveFileAsync(file);
                }

                return Ok(new
                {
                    filename = fileName
                });
            }
            catch (System.Exception e)
            {
                return BadRequest(new { error = e.Message, filename="" });
            }
        }
    }
}
