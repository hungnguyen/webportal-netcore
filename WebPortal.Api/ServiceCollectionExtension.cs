using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;
using WebPortal.Api.Policy;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Services;
using WebPortal.Services.Common;
using WebPortal.Utilities.Options;

namespace WebPortal.Api
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<AppSettingsOption>(Configuration.GetSection(AppSettingsOption.AppSettings));

            services.AddDbContext<WebPortalDbContext>(options => options
                .UseSqlServer(Configuration["ConnectiongStrings:WebPortalDb"]));

            services.AddAuthenticationDefault();

            //DI
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IEncryptDecrypt, EncryptDecrypt>();

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

            services.AddControllers()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    o.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebPortal.Api", Version = "v1" });
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSignalR();

            return services;
        }
        public static IServiceCollection AddAuthenticationHttpOnly(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<WebPortalDbContext>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
            }).AddCookie("Cookies", options =>
            {
                options.Cookie.Name = "auth_cookie";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationDefault(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<WebPortalDbContext>()
                .AddDefaultTokenProviders();

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

                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2)
            );
            services.AddAuthorization();

            return services;
        }
    }
}
