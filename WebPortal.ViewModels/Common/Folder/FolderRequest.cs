using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.ViewModels.Common.Folder
{
    public class FolderCreateRequest
    {
        public string Parent { get; set; }
        public string Name { get; set; }
    }
    public class FolderUpdateRequest
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
    public class FolderDeleteRequest
    {
        public string Path { get; set; }
    }
}
