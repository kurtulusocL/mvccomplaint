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
    public class VideoAdsController : Controller
    {
        ApplicationDbContext _db;

        public VideoAdsController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.Where(i => i.IsDeleted == false).OrderBy(i => i.DeletedTime).ToPagedList(page, 10);
                return View(videoAds);
            }
        }

        public ActionResult Detail(int id)
        {
            return View(_db.VideoAdses.Find(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VideoAds model)
        {
            if (model != null && ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.VideoAdses.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.FirstOrDefault(i => i.Id == id);
                if (videoAds != null)
                {
                    return View(videoAds);
                }
                return RedirectToAction("yonas");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VideoAds model)
        {
            if (model != null && ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.VideoAdses.Add(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var videoAds = _db.VideoAdses.Find(id);
                if (videoAds != null)
                {
                    _db.VideoAdses.Remove(videoAds);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}