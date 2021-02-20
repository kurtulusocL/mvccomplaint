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
    public class OfferCompanyController : Controller
    {
        ApplicationDbContext _db;

        public OfferCompanyController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.OfferCompanies.Include("OfferOwner").Include("Country").Include("Category").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);
                return View(offer);
            }
        }

        public ActionResult ReadOffer(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.OfferCompanies.Include("OfferOwner").Include("Country").Include("Category").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 30);
                return View(offer);
            }
        }

        public ActionResult Detail(int id)
        {
            return View(_db.OfferCompanies.Find(id));
        }

        public ActionResult _OfferComment(int? id)
        {
            Session["offerID"] = id;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _OfferComment(Comment model)
        {
            if (model != null && ModelState.IsValid)
            {
                model.OfferCompanyId = Convert.ToInt32(Session["offerID"]);
                using (_db = new ApplicationDbContext())
                {
                    _db.Comments.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Detail/" + Session["offerID"]);
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
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/offer/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.OfferCompanies.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
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
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/offer/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.OfferCompanies.Add(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var offer = _db.OfferCompanies.Include("OfferOwner").Include("Country").Include("Category").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);
                return View(offer);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var offerConfirm = _db.OfferCompanies.Include("OfferOwner").Include("Country").Include("Category").Include("Comments").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 30);
                return View(offerConfirm);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetConfirm(int id)
        {
            var confirm = _db.OfferCompanies.SingleOrDefault(i => i.Id == id);
            confirm.IsConfirm = true;
            _db.SaveChanges();

            return RedirectToAction("ConfirmList");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult OfferDetail(int id)
        {
            return View(_db.OfferCompanies.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var offerDelete = _db.OfferCompanies.Find(id);
                if (offerDelete != null)
                {
                    _db.OfferCompanies.Remove(offerDelete);
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
                var ownerDelete = _db.OfferOwners.Find(id);
                if (ownerDelete != null)
                {
                    _db.OfferOwners.Remove(ownerDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}