using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ev_4_AE.Models;

namespace Ev_4_AE.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult LoginView()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (eva4_db db = new eva4_db())
                {
                    var oUser = (from d in db.Usuario
                                 where d.email == User.Trim() && d.password == Pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalida";
                        return View();
                    }

                    Session["User"] = oUser;
                    Session["name"] = User;

                }

                return RedirectToAction("Interna", "Home");
            }
            catch (Exception ex)
            {
                //  ViewBag.Error = ex.Message;
                ViewBag.Error = "Error de conexión";
                return View();
            }

        }
            

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Session["User"] = null;
            Session["name"] = null;
            return RedirectToAction("LoginView", "Login");
        }
    }
}