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
    [Authorize(Roles = "Admin")]
    public class AdsController : Controller
    {
        ApplicationDbContext _db;

        public AdsController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var ads = _db.Adses.Where(i => i.IsDeleted == false).OrderByDescending(i => i.DeletedTime).ToPagedList(page, 20);
                return View(ads);
            }
        }

        public ActionResult AdsDetail(int id)
        {
            return View(_db.Abouts.Find(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ads model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Adses.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var adsEdit = _db.Adses.FirstOrDefault(i => i.Id == id);
                if (adsEdit != null)
                {
                    return View(adsEdit);
                }
                return RedirectToAction("yonas");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ads model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Adses.Add(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var adsDelete = _db.Adses.Find(id);
                if (adsDelete != null)
                {
                    _db.Adses.Remove(adsDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}