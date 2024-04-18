using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPortal.AdminPage.Helpers;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;

namespace WebPortal.AdminPage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Db context
            services.AddDbContext<WebPortalDbContext>(options =>
                options.UseSqlServer(Configuration.GetSection("ConnectiongStrings")["WebPortalDb"]));

            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<WebPortalDbContext>()
                .AddDefaultTokenProviders();
            //.AddClaimsPrincipalFactory<WpUserClaimsPrincipalFactory>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            //DI
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IEncryptDecrypt, EncryptDecrypt>();
            services.AddScoped<UrlHelper>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IPhraseService, PhraseService>();
            services.AddTransient<IProductTypeService, ProductTypeService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductInCategoryService, ProductInCategoryService>();
            services.AddTransient<IProductFileService, ProductFileService>();
            services.AddTransient<IProductCommentService, ProductCommentService>();
            services.AddTransient<IProductVoteService, ProductVoteService>();
            services.AddTransient<IPromotionService, PromotionService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IWebsiteService, WebsiteService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderItemService, OrderItemService>();
            services.AddTransient<IPageViewService, PageViewService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IAppRoleService, AppRoleService>();
            services.AddTransient<IMailBoxService, MailBoxService>();            

            //Identity config
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2)
            );
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
