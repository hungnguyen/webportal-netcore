using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPortal.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 250, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(maxLength: 250, nullable: true),
                    IsOnline = table.Column<bool>(nullable: true),
                    LastLoginDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 1000, nullable: true),
                    IP = table.Column<string>(maxLength: 15, nullable: true),
                    Browser = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogin",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogin", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRole", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserToken", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Link = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    InCategories = table.Column<string>(maxLength: 250, nullable: true),
                    OrderNumber = table.Column<int>(nullable: false),
                    LanguageID = table.Column<string>(maxLength: 5, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    Detail = table.Column<string>(maxLength: 1000, nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    OrderNumber = table.Column<int>(nullable: false),
                    ParentID = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TypeCode = table.Column<string>(maxLength: 3, nullable: true),
                    IsPopular = table.Column<bool>(nullable: true),
                    Image = table.Column<string>(maxLength: 250, nullable: true),
                    DisplayType = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 500, nullable: true),
                    MetaKey = table.Column<string>(maxLength: 1000, nullable: true),
                    MetaDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    UrlName = table.Column<string>(maxLength: 250, nullable: true),
                    Link = table.Column<string>(maxLength: 500, nullable: true),
                    Icon = table.Column<string>(maxLength: 250, nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    LanguageID = table.Column<string>(maxLength: 5, nullable: true),
                    IsOnTop = table.Column<bool>(nullable: true),
                    IsOnRight = table.Column<bool>(nullable: true),
                    IsOnBottom = table.Column<bool>(nullable: true),
                    IsOnLeft = table.Column<bool>(nullable: true),
                    IsOnCenter = table.Column<bool>(nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    FullName = table.Column<string>(maxLength: 250, nullable: true),
                    IdCard = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Country = table.Column<string>(maxLength: 250, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    LastLoginDate = table.Column<DateTime>(nullable: true),
                    IsOnline = table.Column<bool>(nullable: true),
                    City = table.Column<string>(maxLength: 250, nullable: true),
                    District = table.Column<string>(maxLength: 250, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Image = table.Column<string>(maxLength: 250, nullable: true),
                    IP = table.Column<string>(maxLength: 15, nullable: true),
                    Browser = table.Column<string>(maxLength: 500, nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MailBox",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromEmail = table.Column<string>(maxLength: 250, nullable: true),
                    ToEmail = table.Column<string>(maxLength: 250, nullable: true),
                    Cc = table.Column<string>(maxLength: 250, nullable: true),
                    Bcc = table.Column<string>(maxLength: 250, nullable: true),
                    Subject = table.Column<string>(maxLength: 500, nullable: true),
                    Body = table.Column<string>(type: "ntext", nullable: true),
                    OrderID = table.Column<int>(nullable: true),
                    LanguageID = table.Column<string>(maxLength: 5, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailBox", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PageView",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<int>(nullable: true),
                    DateReported = table.Column<DateTime>(nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageView", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Phrase",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(maxLength: 250, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: true),
                    LanguageID = table.Column<string>(maxLength: 5, nullable: true),
                    IsPin = table.Column<bool>(nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phrase", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true),
                    Image = table.Column<string>(maxLength: 250, nullable: true),
                    UrlName = table.Column<string>(maxLength: 250, nullable: true),
                    ViewCount = table.Column<int>(nullable: true, defaultValue: 0),
                    LikeCount = table.Column<int>(nullable: true, defaultValue: 0),
                    TypeCode = table.Column<string>(maxLength: 3, nullable: true),
                    LanguageID = table.Column<string>(maxLength: 5, nullable: true),
                    IsHot = table.Column<bool>(nullable: true),
                    IsFeature = table.Column<bool>(nullable: true),
                    ReplateProduct = table.Column<string>(maxLength: 250, nullable: true),
                    Text1 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text2 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text3 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text4 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text5 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text6 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text7 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text8 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text9 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text10 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text11 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text12 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text13 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text14 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text15 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text16 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text17 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text18 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text19 = table.Column<string>(maxLength: 1000, nullable: true),
                    Text20 = table.Column<string>(maxLength: 1000, nullable: true),
                    Desc1 = table.Column<string>(type: "ntext", nullable: true),
                    Desc2 = table.Column<string>(type: "ntext", nullable: true),
                    Desc3 = table.Column<string>(type: "ntext", nullable: true),
                    Desc4 = table.Column<string>(type: "ntext", nullable: true),
                    Desc5 = table.Column<string>(type: "ntext", nullable: true),
                    Desc6 = table.Column<string>(type: "ntext", nullable: true),
                    Desc7 = table.Column<string>(type: "ntext", nullable: true),
                    Desc8 = table.Column<string>(type: "ntext", nullable: true),
                    Desc9 = table.Column<string>(type: "ntext", nullable: true),
                    Desc10 = table.Column<string>(type: "ntext", nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 3, nullable: true),
                    LanguageID = table.Column<string>(maxLength: 5, nullable: true),
                    IsPublic = table.Column<bool>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Text1 = table.Column<string>(maxLength: 250, nullable: true),
                    Text2 = table.Column<string>(maxLength: 250, nullable: true),
                    Text3 = table.Column<string>(maxLength: 250, nullable: true),
                    Text4 = table.Column<string>(maxLength: 250, nullable: true),
                    Text5 = table.Column<string>(maxLength: 250, nullable: true),
                    Text6 = table.Column<string>(maxLength: 250, nullable: true),
                    Text7 = table.Column<string>(maxLength: 250, nullable: true),
                    Text8 = table.Column<string>(maxLength: 250, nullable: true),
                    Text9 = table.Column<string>(maxLength: 250, nullable: true),
                    Text10 = table.Column<string>(maxLength: 250, nullable: true),
                    Text11 = table.Column<string>(maxLength: 250, nullable: true),
                    Text12 = table.Column<string>(maxLength: 250, nullable: true),
                    Text13 = table.Column<string>(maxLength: 250, nullable: true),
                    Text14 = table.Column<string>(maxLength: 250, nullable: true),
                    Text15 = table.Column<string>(maxLength: 250, nullable: true),
                    Text16 = table.Column<string>(maxLength: 250, nullable: true),
                    Text17 = table.Column<string>(maxLength: 250, nullable: true),
                    Text18 = table.Column<string>(maxLength: 250, nullable: true),
                    Text19 = table.Column<string>(maxLength: 250, nullable: true),
                    Text20 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc1 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc2 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc3 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc4 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc5 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc6 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc7 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc8 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc9 = table.Column<string>(maxLength: 250, nullable: true),
                    Desc10 = table.Column<string>(maxLength: 250, nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ApplyForAll = table.Column<bool>(nullable: true),
                    DiscountPercent = table.Column<int>(nullable: true),
                    DiscountAmount = table.Column<decimal>(nullable: true),
                    ApplyForProductIDs = table.Column<string>(maxLength: 500, nullable: true),
                    ApplyForCategories = table.Column<string>(maxLength: 500, nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    WebisteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Website",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Domain = table.Column<string>(maxLength: 250, nullable: true),
                    Folder = table.Column<string>(maxLength: 250, nullable: true),
                    MobileFolder = table.Column<string>(maxLength: 250, nullable: true),
                    DomainAlias = table.Column<string>(maxLength: 250, nullable: true),
                    FromEmail = table.Column<string>(maxLength: 250, nullable: true),
                    SMTPServer = table.Column<string>(maxLength: 50, nullable: true),
                    SMTPServerPort = table.Column<int>(maxLength: 5, nullable: false),
                    SMTPUserName = table.Column<string>(maxLength: 50, nullable: true),
                    SMTPUserPassword = table.Column<string>(maxLength: 50, nullable: true),
                    SMTPSSL = table.Column<bool>(maxLength: 5, nullable: false),
                    Currency = table.Column<string>(maxLength: 3, nullable: true),
                    UploadFolder = table.Column<string>(maxLength: 250, nullable: true),
                    DeliveryFee = table.Column<decimal>(nullable: false),
                    TotalPageView = table.Column<int>(nullable: true, defaultValue: 0),
                    ProjectName = table.Column<string>(maxLength: 250, nullable: true),
                    ProjectLink = table.Column<string>(maxLength: 250, nullable: true),
                    IsDown = table.Column<bool>(nullable: true, defaultValue: false),
                    PageDown = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Website", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatus = table.Column<int>(nullable: false),
                    PromotionCode = table.Column<string>(maxLength: 50, nullable: true),
                    Discount = table.Column<decimal>(nullable: true),
                    TotalAmout = table.Column<decimal>(nullable: true),
                    Fee = table.Column<decimal>(nullable: true),
                    TotalNetAmount = table.Column<decimal>(nullable: true),
                    PayMethod = table.Column<int>(nullable: false),
                    PayStatus = table.Column<int>(nullable: false),
                    ShippingName = table.Column<string>(maxLength: 250, nullable: true),
                    ShippingAddress = table.Column<string>(maxLength: 500, nullable: true),
                    ShippingPhone = table.Column<string>(maxLength: 50, nullable: true),
                    ShippingEmail = table.Column<string>(maxLength: 250, nullable: true),
                    FindUs = table.Column<string>(maxLength: 250, nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: true),
                    DateCompleted = table.Column<DateTime>(nullable: true),
                    DateUpdate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    SaleID = table.Column<Guid>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true),
                    WebsiteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_AppUser_SaleID",
                        column: x => x.SaleID,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    SessionID = table.Column<Guid>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cart_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductComment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: true),
                    Subject = table.Column<string>(maxLength: 250, nullable: true),
                    Message = table.Column<string>(maxLength: 250, nullable: true),
                    ParentID = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LikeCount = table.Column<int>(nullable: true, defaultValue: 0),
                    PeopleLike = table.Column<string>(maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductComment_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductComment_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductFile",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Detail = table.Column<string>(maxLength: 1000, nullable: true),
                    FileName = table.Column<string>(maxLength: 250, nullable: true),
                    Link = table.Column<string>(maxLength: 500, nullable: true),
                    OrderNumber = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductFile_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInCategory",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInCategory", x => new { x.ProductID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_ProductInCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInCategory_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVote",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    CusomterID = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVote", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductVote_Customer_CusomterID",
                        column: x => x.CusomterID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVote_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    ExternalID = table.Column<string>(maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    Fee = table.Column<decimal>(nullable: true),
                    Status = table.Column<string>(maxLength: 250, nullable: true),
                    Message = table.Column<string>(maxLength: 1000, nullable: true),
                    Result = table.Column<string>(maxLength: 250, nullable: true),
                    Provider = table.Column<string>(maxLength: 250, nullable: true),
                    OrderID = table.Column<int>(nullable: true),
                    WebsiteID = table.Column<int>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("155407e9-e21f-42c6-a92b-027bf51074b2"), "a5d6bd6c-cf3f-40fe-8958-2e420f9df576", null, "Administrator", "administrator" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "Browser", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IP", "Image", "IsOnline", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("fe502d13-7276-4233-af6f-f6719ae2444b"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "fccd54ad-e66c-4e66-aad4-f23d1800802c", "hung.nguyen1610@gmail.com", true, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "hung.nguyen1610@gmail.com", "admin", null, "AQAAAAEAACcQAAAAEF5+aZvm1lau33yWOfrnuKRH3ScOGLhdneqa+6tT1aLmx5Hd0MdOOUPmm7+45RRTmA==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("fe502d13-7276-4233-af6f-f6719ae2444b"), new Guid("155407e9-e21f-42c6-a92b-027bf51074b2") });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "ID", "Code", "IsDefault", "Name", "WebsiteID" },
                values: new object[,]
                {
                    { 1, "vi-VN", true, "Tiếng Việt", null },
                    { 2, "en-US", false, "English", null }
                });

            migrationBuilder.InsertData(
                table: "Website",
                columns: new[] { "ID", "Currency", "DeliveryFee", "Domain", "DomainAlias", "Folder", "FromEmail", "MobileFolder", "Name", "PageDown", "ProjectLink", "ProjectName", "SMTPSSL", "SMTPServer", "SMTPServerPort", "SMTPUserName", "SMTPUserPassword", "UploadFolder" },
                values: new object[] { 1, null, 0m, null, null, null, null, null, "DefaultWebsite", null, null, null, false, null, 0, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerID",
                table: "Cart",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductID",
                table: "Cart",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_SaleID",
                table: "Order",
                column: "SaleID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductID",
                table: "OrderItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_CustomerID",
                table: "ProductComment",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_ProductID",
                table: "ProductComment",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFile_ProductID",
                table: "ProductFile",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCategory_CategoryID",
                table: "ProductInCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVote_CusomterID",
                table: "ProductVote",
                column: "CusomterID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVote_ProductID",
                table: "ProductVote",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerID",
                table: "Transaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrderID",
                table: "Transaction",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "AppRoleClaim");

            migrationBuilder.DropTable(
                name: "AppUserClaim");

            migrationBuilder.DropTable(
                name: "AppUserLogin");

            migrationBuilder.DropTable(
                name: "AppUserRole");

            migrationBuilder.DropTable(
                name: "AppUserToken");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "MailBox");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "PageView");

            migrationBuilder.DropTable(
                name: "Phrase");

            migrationBuilder.DropTable(
                name: "ProductComment");

            migrationBuilder.DropTable(
                name: "ProductFile");

            migrationBuilder.DropTable(
                name: "ProductInCategory");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "ProductVote");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Website");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
