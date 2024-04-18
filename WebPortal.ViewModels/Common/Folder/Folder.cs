using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.ViewModels.Common.Folder
{
    public class Folder : ITrackable
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Parent { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
