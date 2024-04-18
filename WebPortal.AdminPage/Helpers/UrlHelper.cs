using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.AdminPage.Helpers
{
    public class UrlHelper
    {
        private readonly IConfiguration configuration;
        public UrlHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetProductUrl(string typeCode, string urlName)
        {
            string homeUrl = configuration["AppSettings:HomePageUrl"];
            string urlType = "";
            switch (typeCode)
            {
                case "PRD":
                    urlType = "product";
                    break;
                default:
                case "NWS":
                    urlType = "news";
                    break;
            }
            return $"{homeUrl}{urlType}/index/{urlName}";
        }
    }
}
