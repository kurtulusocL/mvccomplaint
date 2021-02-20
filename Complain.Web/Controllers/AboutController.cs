using Complain.Data;
using Complain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Complain.Web.Controllers
{
    public class AboutController : Controller
    {
        ApplicationDbContext _db;

        public AboutController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).ToList();
                return View(about);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas()
        {
            using (_db = new ApplicationDbContext())
            {
                var about = _db.Abouts.Where(i => i.IsDeleted == false).ToList();
                return View(about);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AboutDetail(int id)
        {
            return View(_db.Abouts.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(About model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Abouts.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var aboutEdit = _db.Abouts.FirstOrDefault(i => i.Id == id);
                if (aboutEdit != null)
                {
                    return View(aboutEdit);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(About model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Abouts.Add(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var aboutDelete = _db.Abouts.Find(id);
                if (aboutDelete != null)
                {
                    _db.Abouts.Remove(aboutDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}