using AplicacionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aplicacionWeb.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult login21()
        {
            LoginCLS l = new LoginCLS { id = 2, usuario = "Leonel", contraseña = "Messi" };
            return View(l);
        }
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* estructura login*/
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(logindb objUser)
        {
            if (ModelState.IsValid)
            {
                using (PWBDEntities db = new PWBDEntities())
                {
                    var obj = db.logindb.Where(a => a.usuario.Equals(objUser.usuario) && a.contraseña.Equals(objUser.contraseña)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.id.ToString();
                        Session["UserName"] = obj.usuario.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(objUser);
        }



        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* cerrar sesion. */
        public ActionResult Logaut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }


    }
}
