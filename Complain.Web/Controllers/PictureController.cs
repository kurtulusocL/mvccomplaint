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
    public class PictureController : Controller
    {
        ApplicationDbContext _db;

        public PictureController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult yonas(int? id, int page=1)
        {
            using (_db=new ApplicationDbContext())
            {
                var picture = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ProductId == id).OrderByDescending(i => i.Product.ProductName).ToPagedList(page, 30);
                return View(picture);
            }
        }

        public ActionResult ConfirmList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var picture = _db.Pictures.Include("Product").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 30);
                return View(picture);
            }
        }

        public ActionResult GetConfirm(int id)
        {
            var confirm = _db.Pictures.FirstOrDefault(i => i.Id == id);
            confirm.IsConfirm = true;
            _db.SaveChanges();

            return View(confirm);
        }

        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var photoDelete = _db.Pictures.Find(id);
                if (photoDelete!=null)
                {
                    _db.Pictures.Remove(photoDelete);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}