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
    public class ContactController : Controller
    {
        ApplicationDbContext _db;

        public ContactController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var contact = _db.Contacts.Where(i => i.IsDeleted == false).ToList();
                return View(contact);
            }
        }

        public ActionResult _CreateContact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CreateContact(Contact model)
        {
            if (model != null && ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.Contacts.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var contact = _db.Contacts.Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 40);
                return View(contact);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ContactDetail(int id)
        {
            return View(_db.Contacts.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var contactDelete = _db.Contacts.Find(id);
                if (contactDelete != null)
                {
                    _db.Contacts.Remove(contactDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}