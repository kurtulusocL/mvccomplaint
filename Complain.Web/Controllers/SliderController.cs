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
    public class SliderController : Controller
    {
        ApplicationDbContext _db;

        public SliderController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var slider = _db.Sliders.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);
                return View(slider);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/slider/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Sliders.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var sliderEdit = _db.Sliders.FirstOrDefault(i => i.Id == id);
                if (sliderEdit != null)
                {
                    return View(sliderEdit);
                }
                return RedirectToAction("yonas");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/slider/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Sliders.Add(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var sliderDelete = _db.Sliders.Find(id);
                if (sliderDelete != null)
                {
                    _db.Sliders.Remove(sliderDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}