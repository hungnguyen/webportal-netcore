using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.ViewModels.Common.File
{
    public class FileUpdateRequest
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
    public class FileCreateRequest
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IFormFile File { get; set; }
    }
}
