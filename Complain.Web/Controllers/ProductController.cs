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
    public class ProductController : Controller
    {
        ApplicationDbContext _db;

        public ProductController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var product = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);

                return View(product);
            }
        }

        public ActionResult ReadComplain(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var product = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 30);

                return View(product);
            }
        }

        public ActionResult Detail(int id)
        {
            return View(_db.Products.Find(id));
        }

        public ActionResult _ProductComment(int? id)
        {
            Session["productID"] = id;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ProductComment(Comment model)
        {
            if (model != null && ModelState.IsValid)
            {
                model.ProductId = Convert.ToInt32(Session["productID"]);
                using (_db = new ApplicationDbContext())
                {
                    _db.Comments.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Detail/" + Session["productID"]);
        }

        public ActionResult ProductCreate()
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Countries = _db.Countries.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(Product model)
        {
            if (model != null && ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.Products.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();

                }
            }
            return RedirectToAction("PhotoCreate");
        }

        public ActionResult ProductEdit(int id)
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
                return RedirectToAction("PhotoEdit");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(Product model)
        {
            if (model != null && ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.Products.Add(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();

                }
            }
            return RedirectToAction("PhotoEdit");
        }

        public ActionResult PhotoCreate(int? id)
        {
            Session["productID"] = id;
            var picture = _db.Pictures.Include("Product").FirstOrDefault(i => i.ProductId == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhotoCreate(IEnumerable<HttpPostedFileBase> image)
        {
            Picture productPhoto = new Picture();
            productPhoto.ProductId = Convert.ToInt32(Session["productID"]);
            foreach (var item in image)
            {
                productPhoto.Name = Path.GetFileName(item.FileName);
                productPhoto.ImageUrl = Path.Combine(Server.MapPath("~/img/foto/" + item.FileName));
                item.SaveAs(productPhoto.ImageUrl);
                productPhoto.ImageUrl = productPhoto.Name;
                _db.Pictures.Add(productPhoto);
                _db.Entry(productPhoto).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("ConfirmList");
        }

        public ActionResult PhotoEdit(int id)
        {
            Picture productPhotoUpdate = new Picture();
            productPhotoUpdate.ProductId = Convert.ToInt32(Session["productID"]);

            using (_db = new ApplicationDbContext())
            {
                var updatePhoto = _db.Pictures.Where(i => i.IsDeleted == false && i.ProductId == id).FirstOrDefault(i => i.Id == id);
                if (updatePhoto != null)
                {
                    return View(updatePhoto);
                }
                return RedirectToAction("ConfirmList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhotoEdit(IEnumerable<HttpPostedFileBase> image)
        {
            Picture productPhoto = new Picture();
            productPhoto.ProductId = Convert.ToInt32(Session["productID"]);
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

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var complainList = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);

                return View(complainList);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductDetail(int id)
        {
            return View(_db.Products.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductOwnerDetail(int? id)
        {
            var owner = _db.Products.FirstOrDefault(i => i.ComplainOwnerId == id);
            return View(owner);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var confirmList = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == false).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);

                return View(confirmList);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetConfirm(int id)
        {
            var confirm = _db.Products.SingleOrDefault(i => i.Id == id);
            confirm.IsConfirm = true;
            _db.SaveChanges();

            return RedirectToAction("ConfirmList");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteProduct(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteProduct = _db.Products.Find(id);
                if (deleteProduct != null)
                {
                    _db.Products.Remove(deleteProduct);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteProductDetail(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteDetail = _db.ProductDetails.Find(id);
                if (deleteDetail != null)
                {
                    _db.ProductDetails.Remove(deleteDetail);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePhoto(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deletepicture = _db.Pictures.Find(id);
                if (deletepicture != null)
                {
                    _db.Pictures.Remove(deletepicture);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteOwner(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteOwner = _db.ComplainOwners.Find(id);
                if (deleteOwner != null)
                {
                    _db.ComplainOwners.Remove(deleteOwner);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}