using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebPortal.Data.EF
{
    public class WebPortalDbContextFactory : IDesignTimeDbContextFactory<WebPortalDbContext>
    {
        public WebPortalDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var strCnn = configuration.GetSection("ConnectiongStrings")["WebPortalDb"];

            var optionBuilder = new DbContextOptionsBuilder<WebPortalDbContext>();
            optionBuilder.UseSqlServer(strCnn);

            return new WebPortalDbContext(optionBuilder.Options);
        }        
    }
}
