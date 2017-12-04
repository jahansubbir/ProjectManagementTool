using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using ProjectManagementTool.Context;
using ProjectManagementTool.Models;

namespace ProjectManagementTool.Controllers
{
    public class AssignResourcesController : Controller
    {
        // private ProjectContext db = new ProjectContext();
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;

        private ProjectContext db = new ProjectContext();
        private string userId, designation;
        private int itAdminDesignationId;
        public AssignResourcesController()
        {
             itAdminDesignationId = db.Designations.ToList().Find(a => a.Name.Equals("IT Admin")).Id;

            cookie = context.Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                userId = (cookie["UserId"]).ToString();
                designation = (cookie["Designation"]).ToString();
            }
        }
        // GET: AssignResources
        public ActionResult Index()
        {
            if (userId != null)
            {
                var assignResources = db.AssignResources.Include(a => a.Project).Include(a => a.User);
                /* var assignedResources = from resources in db.AssignResources
                join project in db.Projects on resources.ProjectId equals project.Id
                join user in db.Users on resources.UserId equals user.Id
                join designation in db.Designations on user.DesignationId equals designation.Id where 
                select new {AssignResource = resources, User = user, Project = project, Designation = designation};*/
                ViewBag.Designations = db.Designations.ToList();

                return View(assignResources.ToList());
            }
            TempData["message"] = "please login to continue";
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ViewProjecInvolved()
        {
            if (userId != null)
            {
                var assignResources = db.AssignResources.Include(a => a.Project).Include(a => a.User).DistinctBy(a => a.ProjectId);
                ViewBag.AssignResources = assignResources;
                /* var assignedResources = from resources in db.AssignResources
                join project in db.Projects on resources.ProjectId equals project.Id
                join user in db.Users on resources.UserId equals user.Id
                join designation in db.Designations on user.DesignationId equals designation.Id where 
                select new {AssignResource = resources, User = user, Project = project, Designation = designation};*/
                ViewBag.Designations = db.Designations.ToList();
                ViewBag.Members = db.Users.ToList();

                ViewBag.MembersCount = db.AssignResources.Include(a => a.Project).Include(a => a.User);
                ViewBag.ProjectStatus = db.ProjectStatus.ToList();
                return View(assignResources.ToList());
            }
            TempData["message"] = "please login to continue";
            return RedirectToAction("Login", "Home");

        }
        // GET: AssignResources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignResource assignResource = db.AssignResources.Find(id);
            if (assignResource == null)
            {
                return HttpNotFound();
            }
            return View(assignResource);
        }

        // GET: AssignResources/Create
        public ActionResult Create()
        {
            if (userId != null && designation.Equals("Project Manager"))
            {
                
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
                ViewBag.UserId = new SelectList(db.Users.ToList().Where(a=>a.DesignationId!=itAdminDesignationId), "Id", "Name");
                return View();
            }
            TempData["message"] = "Access deined";
            return RedirectToAction("Index", "Home");
        }

        // POST: AssignResources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,UserId")] AssignResource assignResource)
        {
            if (ModelState.IsValid)
            {
                if (
                    db.AssignResources.ToList()
                        .Find(a => a.UserId == assignResource.UserId && a.ProjectId == assignResource.ProjectId) == null)
                {


                    db.AssignResources.Add(assignResource);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Your trying to assign member who is already assigned";
                    ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", assignResource.ProjectId);
                    ViewBag.UserId = new SelectList(db.Users.ToList().Where(a => a.DesignationId != itAdminDesignationId), "Id", "Name", assignResource.UserId);
                    return View(assignResource);
                }
                
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", assignResource.ProjectId);
            ViewBag.UserId = new SelectList(db.Users.ToList().Where(a => a.DesignationId != itAdminDesignationId), "Id", "Name", assignResource.UserId);
            return View(assignResource);
        }

        // GET: AssignResources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (userId != null && designation.Equals("IT Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AssignResource assignResource = db.AssignResources.Find(id);
                if (assignResource == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", assignResource.ProjectId);
                ViewBag.UserId = new SelectList(db.Users.ToList().Where(a => a.DesignationId != itAdminDesignationId), "Id", "Name", assignResource.UserId);
                return View(assignResource);
            }
            TempData["message"] = "Access deined";
            return RedirectToAction("Index", "Home");
        }

        // POST: AssignResources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,UserId")] AssignResource assignResource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignResource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", assignResource.ProjectId);
            ViewBag.UserId = new SelectList(db.Users.ToList().Where(a => a.DesignationId != itAdminDesignationId), "Id", "Name", assignResource.UserId);
            return View(assignResource);
        }

        // GET: AssignResources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (userId != null && designation.Equals("IT Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AssignResource assignResource = db.AssignResources.Find(id);
                if (assignResource == null)
                {
                    return HttpNotFound();
                }
                return View(assignResource);
            }
            TempData["message"] = "Access deined";
            return RedirectToAction("Index", "Home");
        }

        // POST: AssignResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignResource assignResource = db.AssignResources.Find(id);
            db.AssignResources.Remove(assignResource);
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
