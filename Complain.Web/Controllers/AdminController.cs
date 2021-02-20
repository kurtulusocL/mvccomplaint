using Complain.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Complain.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext _db;

        public AdminController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var admin = _db.Users.OrderByDescending(i => i.Id).ToPagedList(page, 20);
                return View(admin);
            }
        }

        public ActionResult HelperList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var helper = _db.Users.OrderByDescending(i => i.Id).ToPagedList(page, 20);
                return View(helper);
            }
        }

        public ActionResult AsistanList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var asistan = _db.Users.OrderByDescending(i => i.Id).ToPagedList(page, 20);
                return View(asistan);
            }
        }

        public ActionResult Delete(string id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleting = _db.Users.Find(id);
                if (deleting != null)
                {
                    _db.Users.Remove(deleting);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}