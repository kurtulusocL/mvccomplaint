using Complain.Data;
using Complain.Entities.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Complain.Web.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _db;

        public CategoryController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var cate = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).ToPagedList(page, 20);
                return View(cate);
            }
        }

        public ActionResult ComplainCategory(int? id, int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var product = _db.Products.Include("Category").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.CategoryId == id).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);

                return View(product);
            }
        }

        public ActionResult OfferCategory(int? id, int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.OfferCompanies.Include("OfferOwner").Include("Category").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.CategoryId == id).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);
                return View(offer);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var cate = _db.Categories.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToPagedList(page, 20);
                return View(cate);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("/img/category/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Categories.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var cateUpdate = _db.Categories.SingleOrDefault(i => i.Id == id);
                if (cateUpdate != null)
                {
                    return View(cateUpdate);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("/img/category/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Categories.Add(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var cateDelete = _db.Categories.Find(id);
                if (cateDelete != null)
                {
                    _db.Categories.Remove(cateDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}