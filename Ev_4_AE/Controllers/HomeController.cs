using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ev_4_AE.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Interna()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Externa()
        {
            return View();
        }
    }
}