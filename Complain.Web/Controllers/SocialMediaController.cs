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
    [Authorize(Roles = "Admin")]
    public class SocialMediaController : Controller
    {
        ApplicationDbContext _db;

        public SocialMediaController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult yonas()
        {
            using (_db=new ApplicationDbContext())
            {
                var social = _db.SocialMedias.Where(i => i.IsDeleted == false).ToList();
                return View(social);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SocialMedia model)
        {
            if (model!=null&&ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.SocialMedias.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        public ActionResult Edit(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var socialUpdate = _db.SocialMedias.SingleOrDefault(i => i.Id == id);
                if (socialUpdate!=null)
                {
                    return View(socialUpdate);
                }
                return RedirectToAction("yonas");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SocialMedia model)
        {
            if (model != null && ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.SocialMedias.Add(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("yonas");
        }

        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deleteSocial = _db.SocialMedias.Find(id);
                if (deleteSocial!=null)
                {
                    _db.SocialMedias.Remove(deleteSocial);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}