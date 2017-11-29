using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectManagementTool.Context;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.Controllers
{
    public class UsersController : Controller
    {
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;

        private ProjectContext db = new ProjectContext();
        private string userId, designation;

        public UsersController()
        {

            cookie = context.Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                userId = (cookie["UserId"]).ToString();
                designation = (cookie["Designation"]).ToString();
            }
        }

        // GET: Users
        public ActionResult Index()
        {
            if (userId != null && designation.Equals("IT Admin"))
            {
                var users = db.Users.Include(u => u.Designation);
                return View(users.ToList());
            }
            TempData["message"] = "Only IT admin can View";
            return RedirectToAction("Login", "Home");
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (userId != null && designation.Equals("IT Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            TempData["message"] = "Only IT admin can View";
            return RedirectToAction("Login", "Home");
        
    }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (userId != null && designation.Equals("IT Admin"))
            {
                ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name");
                return View();
            }
            TempData["message"] = "Only IT admin can View";
            return RedirectToAction("Login", "Home");

        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,PasswordHash,Active,DesignationId")] User user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.PasswordHash = user.Email + "123";
            if (ModelState.IsValid)
            {

                db.Users.Add(user);
                int rowEffected = db.SaveChanges();
                if (rowEffected > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

                // return RedirectToAction("Index");
            }

            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", user.DesignationId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (userId != null && designation.Equals("IT Admin"))
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", user.DesignationId);
                return View(user);
            }
            TempData["message"] = "Only IT admin can View";
            return RedirectToAction("Login", "Home");

        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,PasswordHash,Active,DesignationId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Name", user.DesignationId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (userId != null && designation.Equals("IT Admin"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        TempData["message"] = "Only IT admin can View";
            return RedirectToAction("Login", "Home");

    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
