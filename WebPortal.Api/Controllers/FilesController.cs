using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Services.Common;
using WebPortal.Services.Extensions;
using WebPortal.Utilities.Options;
using WebPortal.ViewModels.Common.File;

namespace WebPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly string _root;
        private readonly IStorageService _storageService;
        public FilesController(IOptions<AppSettingsOption> appSettingsOption, IStorageService storageService)
        {
            _root = appSettingsOption.Value.UploadRootFolder;
            _storageService = storageService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var path = _root;
            if (!Directory.Exists(path))
            {
                return BadRequest($"This path doesn't exist: {path}");
            }
            var files = Directory.GetFiles(path).Select(p => new WebPortal.ViewModels.Common.File.File
            {
                Name = Path.GetFileName(p),
                Path = p.GetFolderPath(_root)
            });
            return Ok(files);
        }

        [HttpGet("sub")]
        public IActionResult Get([FromQuery]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("'Name' cannot be null or empty");
            }

            var path = Path.Combine(_root, name);
            if (!Directory.Exists(path))
            {
                return BadRequest($"This path doesn't exist: {path}");
            }
            var files = Directory.GetFiles(path).Select(p => new WebPortal.ViewModels.Common.File.File
            {
                Name = Path.GetFileName(p),
                Path = p.GetFolderPath(_root)
            });
            return Ok(files);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]FileCreateRequest request)
        {
            try
            {
                string fileName = "";

                if (request.File != null)
                {
                    fileName = await _storageService.SaveFileAsync(request.File, request.Name, request.Path);
                }

                return Ok(new WebPortal.ViewModels.Common.File.File
                {
                    Name = Path.GetFileName(request.File.FileName),
                    Path = request.Path
                });
            }
            catch (System.Exception e)
            {
                return BadRequest(new { error = e.Message, filename = "" });
            }
        }

        [HttpPut]
        public IActionResult Put([FromForm] FileUpdateRequest request) 
        {
            if (string.IsNullOrEmpty(request.Path) 
                || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("'Name' or 'Path' cannot be null or empty");
            }
            var fileInfo = new FileInfo(Path.Combine(_root, request.Path));
            if (!fileInfo.Exists)
            {
                return BadRequest($"File is not existing: {request.Path}.");
            }

            var newFileInfo = new FileInfo(Path.Combine(Path.GetDirectoryName(fileInfo.FullName), request.Name));
            if (newFileInfo.Exists)
            {
                return BadRequest($"File already exist: {request.Name}.");
            }

            try
            {
                
                fileInfo.MoveTo(newFileInfo.FullName);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("remove")]
        public IActionResult Delete([FromQuery]string name) 
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("'Name' cannot be null or empty");
            }

            var fileInfo = new FileInfo(Path.Combine(_root, name));
            if (!fileInfo.Exists)
            {
                return BadRequest($"File is not existing: {name}.");
            }

            try
            {
                fileInfo.Delete();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
