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
    public class ViewProjectDetailsController : Controller
    {
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;

        private ProjectContext db = new ProjectContext();
        private string userId, designation;

        public ViewProjectDetailsController()
        {

            cookie = context.Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                userId = (cookie["UserId"]).ToString();
                designation = (cookie["Designation"]).ToString();

            }
        }


        // GET: ViewProjectDetails
        public ActionResult Index()
        {
            var projectDetails = db.ViewProjectDetails.ToList().Where(a=>a.UserId==userId);
           
            return View();
        }

        // GET: ViewProjectDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewProjectDetails viewProjectDetails = db.ViewProjectDetails.Find(id);
            if (viewProjectDetails == null)
            {
                return HttpNotFound();
            }
            return View(viewProjectDetails);
        }

        // GET: ViewProjectDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewProjectDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Project,CodeName,Status,AssignedTo,Task")] ViewProjectDetails viewProjectDetails)
        {
            if (ModelState.IsValid)
            {
                db.ViewProjectDetails.Add(viewProjectDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewProjectDetails);
        }

        // GET: ViewProjectDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewProjectDetails viewProjectDetails = db.ViewProjectDetails.Find(id);
            if (viewProjectDetails == null)
            {
                return HttpNotFound();
            }
            return View(viewProjectDetails);
        }

        // POST: ViewProjectDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Project,CodeName,Status,AssignedTo,Task")] ViewProjectDetails viewProjectDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewProjectDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewProjectDetails);
        }

        // GET: ViewProjectDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewProjectDetails viewProjectDetails = db.ViewProjectDetails.Find(id);
            if (viewProjectDetails == null)
            {
                return HttpNotFound();
            }
            return View(viewProjectDetails);
        }

        // POST: ViewProjectDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewProjectDetails viewProjectDetails = db.ViewProjectDetails.Find(id);
            db.ViewProjectDetails.Remove(viewProjectDetails);
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
