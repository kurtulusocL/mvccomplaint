namespace Complain.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Subtitle = c.String(),
                        Subdetail = c.String(),
                        Detail = c.String(),
                        Text = c.String(),
                        Photo = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Description = c.String(),
                        WebSite = c.String(),
                        Photo = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Careers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentityNo = c.String(),
                        PassportNo = c.String(),
                        NameSurname = c.String(nullable: false),
                        ShortIntroduction = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Nationality = c.String(nullable: false),
                        Gender = c.String(),
                        UniversityName = c.String(nullable: false),
                        DepartmanName = c.String(nullable: false),
                        KnowingLenguage = c.String(nullable: false),
                        MailAddress = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        LinkedIn = c.String(),
                        Province = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Folder = c.String(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Photo = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubTitle = c.String(),
                        ProductName = c.String(),
                        Subject = c.String(),
                        Detail = c.String(),
                        IsConfirm = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        ProductDetailId = c.Int(nullable: false),
                        ComplainOwnerId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ComplainOwners", t => t.ComplainOwnerId, cascadeDelete: true)
                .ForeignKey("dbo.ProductDetails", t => t.ProductDetailId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.ProductDetailId)
                .Index(t => t.ComplainOwnerId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(nullable: false),
                        MailAddress = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Text = c.String(nullable: false, maxLength: 250),
                        IsConfirm = c.Boolean(nullable: false),
                        ProductId = c.Int(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ComplainOwners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        MailAddress = c.String(),
                        Province = c.String(),
                        PhoneNumber = c.String(),
                        Gender = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        CountryId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Photo = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        Photo = c.String(),
                        IsConfirm = c.Boolean(nullable: false),
                        ProductId = c.Int(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        UsedTime = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OnBought = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(nullable: false),
                        MailAddress = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Text = c.String(nullable: false, maxLength: 250),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfferCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Description = c.String(),
                        WebSite = c.String(),
                        Photo = c.String(),
                        VideoLink = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Photo = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Instagram = c.String(),
                        LinkedIn = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NameLastname = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VideoAds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        WebSite = c.String(),
                        VideoLink = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        DeletedTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "ProductDetailId", "dbo.ProductDetails");
            DropForeignKey("dbo.Pictures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ComplainOwnerId", "dbo.ComplainOwners");
            DropForeignKey("dbo.ComplainOwners", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropIndex("dbo.Pictures", new[] { "ProductId" });
            DropIndex("dbo.ComplainOwners", new[] { "CountryId" });
            DropIndex("dbo.Comments", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ComplainOwnerId" });
            DropIndex("dbo.Products", new[] { "ProductDetailId" });
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.VideoAds");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SocialMedias");
            DropTable("dbo.Sliders");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OfferCompanies");
            DropTable("dbo.Contacts");
            DropTable("dbo.SubCategories");
            DropTable("dbo.ProductDetails");
            DropTable("dbo.Pictures");
            DropTable("dbo.Countries");
            DropTable("dbo.ComplainOwners");
            DropTable("dbo.Comments");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Careers");
            DropTable("dbo.Ads");
            DropTable("dbo.Abouts");
        }
    }
}
