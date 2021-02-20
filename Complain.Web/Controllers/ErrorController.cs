using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Complain.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Page403()
        {
            Response.StatusCode = 403;
            return View();
        }

        public ActionResult Page500()
        {
            Response.StatusCode = 500;
            return View();
        }        
    }
}