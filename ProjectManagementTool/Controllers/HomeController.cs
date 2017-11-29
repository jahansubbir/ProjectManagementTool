using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManagementTool.Context;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.Controllers
{
    public class HomeController : Controller
    {
        ProjectContext db =new ProjectContext();
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            cookie = new HttpCookie("loginCookie");
            var users = db.Users.ToList();
            var user = users.Find(a => a.Email.Equals(model.Email) && a.PasswordHash == model.Password);
            if (user != null)
            {
                //HttpCookie cookie=
                cookie["UserId"] = user.Id;
                cookie["UserName"] = user.Name;
                var designation = db.Designations.ToList().Find(a => a.Id == user.DesignationId).Name;
                cookie["Designation"] = designation;
                cookie.Expires = DateTime.Now.AddDays(2);

                Response.Cookies.Add(cookie);


                return RedirectToAction("Index");
            }
            TempData["message"] = "Invalid Login Credentials";
            return View();

        }

        public ActionResult LogOff()
        {
            cookie = context.Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                cookie["UserId"] = null;
                cookie["UserName"] = null;
                cookie["Designation"] = null;
                cookie.Expires = DateTime.Now;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index");
        }
    }
}