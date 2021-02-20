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
    public class CountryController : Controller
    {
        ApplicationDbContext _db;

        public CountryController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var country = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToPagedList(page, 10);
                return View(country);
            }
        }

        public ActionResult ComplainCountry(int? id, int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var product = _db.Products.Include("Category").Include("SubCategory").Include("ProductDetail").Include("ComplainOwner").Include("Pictures").Include("Comments").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.CountryId == id).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 20);

                return View(product);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var country = _db.Countries.Include("Products").Include("OfferCompanies").Where(i => i.IsDeleted == false).OrderBy(i => i.Products.Count()).ToPagedList(page, 15);
                return View(country);
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
        public ActionResult Create(Country model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/country/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Countries.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var countryUpdate = _db.Countries.SingleOrDefault(i => i.Id == id);
                if (countryUpdate!=null)
                {
                    return View(countryUpdate);
                }
                return RedirectToAction("yonas");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/country/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Countries.Add(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("yonas");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var countryDelete = _db.Countries.Find(id);
                if (countryDelete!=null)
                {
                    _db.Countries.Remove(countryDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}