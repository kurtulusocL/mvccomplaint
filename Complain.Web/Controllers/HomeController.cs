using Complain.Data;
using Complain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Complain.Web.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserArgeement()
        {
            return View();
        }

        public ActionResult UserSecurity()
        {
            return View();
        }

        public ActionResult KVKHK()
        {
            return View();
        }

        [Route("sitemap.xml")]
        public ActionResult SitemapXml()
        {
            ApplicationDbContext veri = new ApplicationDbContext();

            Response.Clear();
            Response.ContentType = "text/xml";
            XmlTextWriter xr = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            xr.WriteStartDocument();
            xr.WriteStartElement("urlset");
            xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");

            xr.WriteStartElement("url");
            xr.WriteElementString("loc", "http://localhost:59252/");
            xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            xr.WriteElementString("changefreq", "daily");
            xr.WriteElementString("priority", "1");
            xr.WriteEndElement();

            var p = veri.Abouts;
            foreach (var b in p)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/About/" + b.Title);
                xr.WriteElementString("loc", "http://localhost:59252/About/" + b.Subtitle);
                xr.WriteElementString("loc", "http://localhost:59252/About/" + b.Subdetail);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var z = veri.Categories;
            foreach (var b in z)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/Category/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var zq = veri.Products;
            foreach (var b in zq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/Car/" + b.Title);
                xr.WriteElementString("loc", "http://localhost:59252/Car/" + b.CompanyName);
                xr.WriteElementString("loc", "http://localhost:59252/Car/" + b.ProductName);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var zaq = veri.ProductDetails;
            foreach (var b in zaq)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/CarDetail/" + b.Brand);
                xr.WriteElementString("loc", "http://localhost:59252/CarDetail/" + b.UsedTime);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var zu = veri.Comments;
            foreach (var b in zu)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/Comment/" + b.Subject);
                xr.WriteElementString("loc", "http://localhost:59252/Comment/" + b.Text);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var ku = veri.Countries;
            foreach (var b in ku)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/Country/" + b.Name);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var vax = veri.OfferCompanies;
            foreach (var b in vax)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/OfferCompany/" + b.CompanyName);
                xr.WriteElementString("loc", "http://localhost:59252/OfferCompany/" + b.Description);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }

            var va = veri.SocialMedias;
            foreach (var b in va)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://localhost:59252/SocialMedia/" + b.Facebook);
                xr.WriteElementString("loc", "http://localhost:59252/SocialMedia/" + b.Instagram);
                xr.WriteElementString("loc", "http://localhost:59252/SocialMedia/" + b.Twitter);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "1");
                xr.WriteElementString("changefreq", "monthly");
                xr.WriteEndElement();
            }
            xr.WriteEndDocument();
            xr.Flush();
            xr.Close();
            Response.End();
            return View();
        }

        public ActionResult OfferedCompany()
        {
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.OfferCompanies.Include("Category").Include("Country").Include("OfferOwner").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderBy(i => i.CreatedTime).Take(4).ToList();
                return PartialView("_OfferedCompany", offer);
            }
        }

        public ActionResult AboutDetail()
        {
            using (_db = new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedTime).ToList();
                return PartialView("_AboutDetail", about);
            }
        }

        public ActionResult CountryShared()
        {
            using (_db = new ApplicationDbContext())
            {
                var country = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToList();
                return PartialView("_CountryShared", country);
            }
        }

        public ActionResult CountryOfferDetail()
        {
            using (_db = new ApplicationDbContext())
            {
                var country = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToList();
                return PartialView("_CountryOfferDetail", country);
            }
        }

        public ActionResult GetCountryForCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var country = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToList();
                return PartialView("_GetCountryForCategory", country);
            }
        }

        public ActionResult GetCategoryForComplain()
        {
            using (_db = new ApplicationDbContext())
            {
                var category = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToList();
                return PartialView("_GetCategoryForComplain", category);
            }
        }

        public ActionResult GetCategoryForOffer()
        {
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.OfferCompanies.Count()).ToList();
                return PartialView("_GetCategoryForOffer", offer);
            }
        }

        public ActionResult GetOfferCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var cateOffer = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.OfferCompanies.Count()).ToList();
                return PartialView("_GetOfferCategory", cateOffer);
            }
        }

        public ActionResult GetComplainCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var cateComplain = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToList();
                return PartialView("_GetComplainCategory", cateComplain);
            }
        }

        public ActionResult CategoryShared()
        {
            using (_db = new ApplicationDbContext())
            {
                var cate = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToList();
                return PartialView("_CategoryShared", cate);
            }
        }

        public ActionResult CategoryOfferDetail()
        {
            using (_db = new ApplicationDbContext())
            {
                var cate = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.OfferCompanies.Count()).ToList();
                return PartialView("_CategoryOfferDetail", cate);
            }
        }

        public ActionResult AdsForCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var adsCate = _db.Adses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Take(5).ToList();
                return PartialView("_AdsForCategory", adsCate);
            }
        }

        public ActionResult AdsForComplainCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var adsComplainCate = _db.Adses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Take(5).ToList();
                return PartialView("_AdsForComplainCategory", adsComplainCate);
            }
        }

        public ActionResult AdsForOffer()
        {
            using (_db = new ApplicationDbContext())
            {
                var adsOffer = _db.Adses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Take(5).ToList();
                return PartialView("_AdsForOffer", adsOffer);
            }
        }

        public ActionResult AdsForCountry()
        {
            using (_db = new ApplicationDbContext())
            {
                var adsCountry = _db.Adses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Take(5).ToList();
                return PartialView("_AdsForCountry", adsCountry);
            }
        }

        public ActionResult AdsOfferDetail()
        {
            using (_db = new ApplicationDbContext())
            {
                var adsCate = _db.Adses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Skip(5).Take(5).ToList();
                return PartialView("_AdsOfferDetail", adsCate);
            }
        }

        public ActionResult VideoAdsForCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Take(1).ToList();
                return PartialView("_VideoAdsForCategory", videoAds);
            }
        }

        public ActionResult VideoAdsForCountry()
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Skip(1).Take(1).ToList();
                return PartialView("_VideoAdsForCountry", videoAds);
            }
        }

        public ActionResult VideoAdsComplainDetail()
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAdsComplain = _db.VideoAdses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Skip(2).Take(1).ToList();
                return PartialView("_VideoAdsComplainDetail", videoAdsComplain);
            }
        }

        public ActionResult VideoAdsForOffered()
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Skip(2).Take(1).ToList();
                return PartialView("_VideoAdsForOffered", videoAds);
            }
        }

        public ActionResult VideoAdsForComplain()
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Take(1).ToList();
                return PartialView("_VideoAdsForComplain", videoAds);
            }
        }

        public ActionResult MostComplainBrand()
        {
            using (_db = new ApplicationDbContext())
            {
                var mostComplain = _db.ProductDetails.Include("Products").Where(i => i.IsDeleted == false).OrderBy(i => i.Brand).Take(10).ToList();

                return PartialView("_MostComplainBrand", mostComplain);
            }
        }

        public ActionResult MostComplainBrandCountry()
        {
            using (_db = new ApplicationDbContext())
            {
                var mostComplain = _db.ProductDetails.Include("Products").Where(i => i.IsDeleted == false).OrderBy(i => i.Brand).Take(10).ToList();

                return PartialView("_MostComplainBrandCountry", mostComplain);
            }
        }

        public ActionResult LastComplainCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var lastComplain = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderBy(i => i.CreatedTime).Take(5).ToList();

                return PartialView("_LastComplainCategory", lastComplain);
            }
        }

        public ActionResult LastComplainCategoryPhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var lastCompainPhoto = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ProductId == id).OrderBy(i => i.Product.ProductName).Take(1).ToList();
                return PartialView("_LastComplainCategoryPhoto", lastCompainPhoto);
            }
        }

        public ActionResult ComplainPhotoInCategory(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var lastCompainPhoto = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ProductId == id).OrderBy(i => i.Product.ProductName).Take(1).ToList();
                return PartialView("_ComplainPhotoInCategory", lastCompainPhoto);
            }
        }

        public ActionResult GetComplainPhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var compainPhoto = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.ProductId == id).OrderBy(i => i.Product.ProductName).Take(1).ToList();
                return PartialView("_GetComplainPhoto", compainPhoto);
            }
        }

        public ActionResult LastOfferedPeople(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var lastOffered = _db.OfferCompanies.Include("OfferOwner").Include("Category").Include("Country").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.OfferOwnerId == id).OrderBy(i => i.CreatedTime).Take(10).ToList();
                return PartialView("_LastOfferedPeople", lastOffered);
            }
        }

        public ActionResult GetCountryForComplain()
        {
            using (_db = new ApplicationDbContext())
            {
                var countryForComplainCategory = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).ToList();
                return PartialView("_GetCountryForComplain", countryForComplainCategory);
            }
        }

        public ActionResult LastComplainPeople(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var lastComplain = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ComplainOwnerId == id).OrderByDescending(i => i.CreatedTime).Take(10).ToList();
                return PartialView("_LastComplainPeople", lastComplain);
            }
        }

        public ActionResult LastOfferedThings()
        {
            using (_db = new ApplicationDbContext())
            {
                var lastOffered = _db.OfferCompanies.Include("OfferOwner").Include("Category").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderBy(i => i.CreatedTime).Take(8).ToList();
                return PartialView("_LastOfferedThings", lastOffered);
            }
        }

        public ActionResult LastComplainThings()
        {
            using (_db = new ApplicationDbContext())
            {
                var lastComplain = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).Take(8).ToList();

                return PartialView("_LastComplainThings", lastComplain);
            }
        }

        public ActionResult LastComplainPhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var photoLastComplain = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ProductId == id).OrderBy(i => i.CreatedTime).Take(1).ToList();
                return PartialView("_LastComplainPhoto", photoLastComplain);
            }
        }

        public ActionResult ComplainDetailPhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var complainPhoto = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ProductId == id).OrderByDescending(i => i.CreatedTime).Take(2).ToList();
                return PartialView("_ComplainDetailPhoto", complainPhoto);
            }
        }

        public ActionResult ComplainDetailPhotoTwo(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var complainPhotoTwo = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ProductId == id).OrderByDescending(i => i.CreatedTime).Skip(2).Take(2).ToList();
                return PartialView("_ComplainDetailPhotoTwo", complainPhotoTwo);
            }
        }

        public ActionResult HomeAbout()
        {
            using (_db = new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).ToList();
                return PartialView("_HomeAbout", about);
            }
        }

        public ActionResult HomeLastOffer()
        {
            using (_db = new ApplicationDbContext())
            {
                var lastOffered = _db.OfferCompanies.Include("OfferOwner").Include("Category").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderBy(i => i.CreatedTime).Take(8).ToList();
                return PartialView("_HomeLastOffer", lastOffered);
            }
        }

        public ActionResult HomeLastComplain()
        {
            using (_db = new ApplicationDbContext())
            {
                var lastComplain = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderBy(i => i.CreatedTime).Take(8).ToList();

                return PartialView("_HomeLastComplain", lastComplain);
            }
        }

        public ActionResult HomeLastComplainPhoto(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var lastComplainPhoto = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.ProductId == id && i.IsConfirm == true).OrderBy(i => i.CreatedTime).Take(1).ToList();
                return PartialView("_HomeLastComplainPhoto", lastComplainPhoto);
            }
        }

        public ActionResult HomeMostComplainCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var mostComplainCate = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).Take(3).ToList();
                return PartialView("_HomeMostComplainCategory", mostComplainCate);
            }
        }

        public ActionResult HomeMostOfferCategory()
        {
            using (_db = new ApplicationDbContext())
            {
                var mostOfferCate = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.OfferCompanies.Count()).Take(3).ToList();
                return PartialView("_HomeMostOfferCategory", mostOfferCate);
            }
        }

        public ActionResult HomeVideoAdsTwo()
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Skip(1).Take(1).ToList();
                return PartialView("_HomeVideoAdsTwo", videoAds);
            }
        }

        public ActionResult HomeAds()
        {
            using (_db = new ApplicationDbContext())
            {
                var homeAds = _db.Adses.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).Take(4).ToList();
                return PartialView("_HomeAds", homeAds);
            }
        }

        public ActionResult HomeComplainIstatistic()
        {
            using (_db = new ApplicationDbContext())
            {
                var complainIstatistic = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderByDescending(i => i.Products.Count()).Take(2).ToList();
                return View("_HomeComplainIstatistic", complainIstatistic);
            }
        }

        public ActionResult HomeOfferIstatistic()
        {
            using (_db = new ApplicationDbContext())
            {
                var offerIstatistic = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderByDescending(i => i.OfferCompanies.Count()).Take(2).ToList();
                return PartialView("_HomeOfferIstatistic", offerIstatistic);
            }
        }

        public ActionResult HomeCountryOfferIstatistic()
        {
            using (_db = new ApplicationDbContext())
            {
                var offerIstatistic = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.OfferCompanies.Count()).Take(2).ToList();
                return PartialView("_HomeCountryOfferIstatistic", offerIstatistic);
            }
        }

        public ActionResult HomeCountryComplainIstatistic()
        {
            using (_db = new ApplicationDbContext())
            {
                var offerIstatistic = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).Take(2).ToList();
                return PartialView("_HomeCountryComplainIstatistic", offerIstatistic);
            }
        }
    }
}