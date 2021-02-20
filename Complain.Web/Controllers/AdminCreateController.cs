using Complain.Data;
using Complain.Entities.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Complain.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCreateController : Controller
    {
        ApplicationDbContext _db;

        public AdminCreateController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult ComplainList(string adminId, int page = 1)
        {
            adminId = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                var product = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);

                return View(product);
            }
        }

        public ActionResult CreateProduct()
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Countries = _db.Countries.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Product model)
        {
            model.UserId = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                _db.Products.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }

            return RedirectToAction("CreatePhoto");
        }

        public ActionResult EditProduct(int id)
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Countries = _db.Countries.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();

            using (_db = new ApplicationDbContext())
            {
                var updateProduct = _db.Products.SingleOrDefault(i => i.Id == id);
                if (updateProduct != null)
                {
                    return View(updateProduct);
                }
                return RedirectToAction("EditPhoto");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product model)
        {
            if (model != null && ModelState.IsValid)
            {
                model.UserId = Convert.ToString(Session["adminId"]);
                using (_db = new ApplicationDbContext())
                {
                    _db.Products.Add(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();

                }
            }
            return RedirectToAction("EditPhoto");
        }

        public ActionResult PhotoList(string adminId, int page = 1)
        {
            adminId = Convert.ToString(Session["adminId"]);

            using (_db = new ApplicationDbContext())
            {
                var picture = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false).OrderByDescending(i => i.Product.ProductName).ToPagedList(page, 30);
                return View(picture);
            }
        }

        public ActionResult CreatePhoto(int? id)
        {
            Session["productID"] = id;
            var picture = _db.Pictures.Include("Product").Where(i => i.ProductId == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePhoto(IEnumerable<HttpPostedFileBase> image)
        {
            Picture productPhoto = new Picture();
            productPhoto.ProductId = Convert.ToInt32(Session["productID"]);
            productPhoto.UserId = Convert.ToString(Session["adminId"]);

            foreach (var item in image)
            {
                productPhoto.Name = Path.GetFileName(item.FileName);
                productPhoto.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                item.SaveAs(productPhoto.ImageUrl);
                productPhoto.ImageUrl = productPhoto.Name;
                _db.Pictures.Add(productPhoto);
                _db.SaveChanges();
            }
            return RedirectToAction("PhotoList");
        }

        public ActionResult EditPhoto(int id)
        {
            Picture productPhotoUpdate = new Picture();
            productPhotoUpdate.ProductId = Convert.ToInt32(Session["productID"]);
            productPhotoUpdate.UserId = Convert.ToString(Session["adminId"]);

            using (_db = new ApplicationDbContext())
            {
                var updatePhoto = _db.Pictures.Where(i => i.IsDeleted == false && i.ProductId == id).SingleOrDefault(i => i.Id == id);
                if (updatePhoto != null)
                {
                    return View(updatePhoto);
                }
                return RedirectToAction("ConfirmList", "Product");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhoto(IEnumerable<HttpPostedFileBase> image)
        {
            Picture productPhoto = new Picture();
            productPhoto.ProductId = Convert.ToInt32(Session["productID"]);
            productPhoto.UserId = Convert.ToString(Session["adminId"]);

            foreach (var item in image)
            {
                productPhoto.Name = Path.GetFileName(item.FileName);
                productPhoto.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                item.SaveAs(productPhoto.ImageUrl);
                productPhoto.ImageUrl = productPhoto.Name;
                _db.Pictures.Add(productPhoto);
                _db.Entry(productPhoto).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("ConfirmList", "Product");
        }

        public ActionResult OfferList(string adminID, int page = 1)
        {
            adminID = Convert.ToString(Session["adminId"]);
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.OfferCompanies.Include("OfferOwner").Include("Country").Include("Category").Include("Comments").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);
                return View(offer);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Countries = _db.Countries.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferCompany model, HttpPostedFileBase image)
        {
            model.UserId = Convert.ToString(Session["adminId"]);
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/offer/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.OfferCompanies.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("OfferList");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Countries = _db.Countries.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.OfferCompanies.SingleOrDefault(i => i.Id == id);
                if (offer != null)
                {
                    return View(offer);
                }
                return RedirectToAction("yonas");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OfferCompany model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Convert.ToString(Session["adminId"]);
                if (image != null && image.ContentLength > 0)
                {
                    image.SaveAs(Server.MapPath("~/img/offer/" + image.FileName));
                    model.Photo = image.FileName;
                }
                _db.OfferCompanies.Add(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("OfferList");
        }
    }
}