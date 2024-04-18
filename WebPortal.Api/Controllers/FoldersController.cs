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
using WebPortal.ViewModels.Common.Folder;

namespace WebPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly string _root;
        public FoldersController(IOptions<AppSettingsOption> appSettingsOption) {
            _root = appSettingsOption.Value.UploadRootFolder;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var path = _root;
            if (!Directory.Exists(path))
            {
                return BadRequest($"This path doesn't exist: {path}");
            }
            var folders = Directory.GetDirectories(path).Select(p => new Folder
            {
                Name = p.GetFolderName(),
                Path = p.GetFolderPath(_root),
                Parent = p.GetParentPath(_root)
            });
            return Ok(folders);
        }

        ///api/folders/sub?name=folder\folder1\folder2
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
                return NoContent();
            }    
            var folders = Directory.GetDirectories(path).Select(p => new Folder { 
                Name = p.GetFolderName(), 
                Path = p.GetFolderPath(_root),
                Parent = p.GetParentPath(_root)
            });
            return Ok(folders);
        }
        [HttpPost]
        public IActionResult Post(FolderCreateRequest request)
        {
            if(string.IsNullOrEmpty(request.Name)) 
            {
                return BadRequest("'Name' cannot be null or empty");
            }

            var parent = Path.Combine(_root, request.Parent);
            if(!Directory.Exists(parent))
            {
                return BadRequest($"Folder is not existing: {request.Parent}.");
            }    

            var path = Path.Combine(parent, request.Name);
            if (Directory.Exists(path))
            {
                return BadRequest($"Folder already exist: {request.Name}.");
            }

            try
            {
                Directory.CreateDirectory(path);
                return Ok(new Folder
                {
                    Name = request.Name,
                    Path = path.GetFolderPath(_root),
                    Parent = path.GetParentPath(_root)
                });
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPut]
        public IActionResult Put(FolderUpdateRequest request) 
        {
            if (string.IsNullOrEmpty(request.Path) 
                || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("'Name' or 'Path' cannot be null or empty");
            }
            var path = Path.Combine(_root, request.Path);
            if (!Directory.Exists(path))
            {
                return BadRequest($"Folder is not existing: {request.Path}.");
            }

            var newPath = Path.Combine(Path.GetDirectoryName(path), request.Name);
            if (Directory.Exists(newPath))
            {
                return BadRequest($"Folder already exist: {request.Name}.");
            }

            try
            {
                //Move all files and subfolders
                Directory.Move(path, newPath);

                return Ok(new Folder
                {
                    Name = request.Name,
                    Path = newPath.GetFolderPath(_root),
                    Parent = newPath.GetParentPath(_root)
                });
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
            var path = Path.Combine(_root, name);
            if (!Directory.Exists(path))
            {
                return BadRequest($"Folder is not existing: {name}.");
            }

            try
            {
                Directory.Delete(path);

                return Ok(name);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
