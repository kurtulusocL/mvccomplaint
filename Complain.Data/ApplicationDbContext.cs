using Complain.Data.Identity;
using Complain.Entities.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complain.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>("DefaultConnection"));
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Ads> Adses { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ComplainOwner> ComplainOwners { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<VideoAds> VideoAdses { get; set; }
        public DbSet<OfferCompany> OfferCompanies { get; set; }
        public DbSet<OfferOwner> OfferOwners { get; set; }
    }
}
