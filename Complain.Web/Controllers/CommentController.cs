using Complain.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Complain.Web.Controllers
{
    public class CommentController : Controller
    {
        ApplicationDbContext _db;

        public CommentController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult CommentProduct(int? id)
        {
            using (_db = new ApplicationDbContext())
            {
                var productComments = _db.Comments.Include("Product").Include("OfferCompany").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.ProductId == id).OrderByDescending(i => i.CreatedTime).Take(30).ToList();

                return PartialView("_CommentProduct", productComments);
            }
        }

        public ActionResult CommentOffer(int? id)
        {
            using (_db=new ApplicationDbContext())
            {
                var offerComment = _db.Comments.Include("Product").Include("OfferCompany").Where(i => i.IsDeleted == false && i.IsConfirm == true && i.OfferCompanyId == id).OrderBy(i => i.CreatedTime).Take(30).ToList();
                return PartialView("_CommentOffer", offerComment);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult yonas(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var comment = _db.Comments.Include("Product").Where(i => i.IsDeleted == false && i.IsConfirm == true).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 30);
                return View(comment);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CommentDetail(int id)
        {
            return View(_db.Comments.Find(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var comment = _db.Comments.Include("Product").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedTime).ToPagedList(page, 30);
                return View(comment);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetConfirm(int id)
        {
            var confirm = _db.Comments.SingleOrDefault(i => i.Id == id);
            confirm.IsConfirm = true;
            _db.SaveChanges();

            return RedirectToAction("ConfirmList");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deleteComment = _db.Comments.Find(id);
                if (deleteComment!=null)
                {
                    _db.Comments.Remove(deleteComment);
                    _db.SaveChanges();
                }
                return RedirectToAction("yonas");
            }
        }
    }
}