using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Utilities.Options
{
    public class AppSettingsOption
    {
        public const string AppSettings = "AppSettings";

        public string UploadRootFolder { get; set; } = String.Empty;
        public string ImageBaseAddess { get; set; } = String.Empty;
        public string FrontEndBaseUrl { get; set; } = "*";
    }
}
