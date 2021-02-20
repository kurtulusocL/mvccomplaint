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
    public class CareerController : Controller
    {
        ApplicationDbContext _db;

        public CareerController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var career = _db.Careers.Where(i => i.IsDeleted == false).ToList();
                return View(career);
            }
        }

        public ActionResult _Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(Career model, HttpPostedFileBase resume)
        {
            if (resume != null && resume.ContentLength > 0)
            {
                resume.SaveAs(Server.MapPath("~/cv/" + resume.FileName));
                model.Folder = resume.FileName;
            }
            _db.Careers.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            var career = _db.Careers.Where(i => i.IsDeleted == false).OrderBy(i => i.CreatedTime).ToPagedList(page, 15);
            return View(career);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ContactInfo(int? id)
        {
            return View(_db.Careers.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DetailCareer(int? id)
        {
            return View(_db.Careers.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCareer(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var career = _db.Careers.Find(id);
                if (career != null)
                {
                    _db.Careers.Remove(career);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}