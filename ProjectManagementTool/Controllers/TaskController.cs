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
    public class TaskController : Controller
    {


        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;

        private ProjectContext db = new ProjectContext();
        private string userId, designation,userName;

        public TaskController()
        {

            cookie = context.Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                userName = (cookie["UserName"]).ToString();
                userId = (cookie["UserId"]).ToString();
                designation = (cookie["Designation"]).ToString();
            }
        }

        // GET: Task
        public ActionResult Index()
        {
            if (userId != null && !designation.Equals("IT Manager"))
            {

                ViewBag.AssignedBy = db.Users.ToList();
                var tasks =
                    db.Tasks.Include(p => p.Priority)
                        .Include(p => p.Project)
                        .Include(p => p.User)
                        
                        .Where(a => a.AssignedBy == userId || a.AssignTo==userId);
                return View(tasks.ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.Tasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
           
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            var adminDesignation = db.Designations.ToList().Find(a => a.Name.Equals("IT Admin"));
            ViewBag.UserId = new SelectList(db.Users.ToList().Where(a=>a.DesignationId!=adminDesignation.Id), "Id", "Name");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,AssignTo,Name,Description,DueDate,PriorityId")] ProjectTask projectTask)
        {
            projectTask.AssignedBy = userId;
            if (ModelState.IsValid)
            {
                db.Tasks.Add(projectTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", projectTask.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectTask.ProjectId);
            var adminDesignation = db.Designations.ToList().Find(a => a.Name.Equals("IT Admin"));
            ViewBag.UserId = new SelectList(db.Users.ToList().Where(a => a.DesignationId != adminDesignation.Id), "Id", "Name",projectTask.User.DesignationId);
            return View(projectTask);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.Tasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", projectTask.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectTask.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", projectTask.AssignTo);
            return View(projectTask);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,AssignTo,Name,Description,DueDate,PriorityId")] ProjectTask projectTask)
        {
            projectTask.AssignedBy = userId;
            if (ModelState.IsValid)
            {
                db.Entry(projectTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", projectTask.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectTask.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", projectTask.AssignTo);
            return View(projectTask);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.Tasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectTask projectTask = db.Tasks.Find(id);
            db.Tasks.Remove(projectTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*public ActionResult ViewProjects()
        {
            /*var projectDetails = db.AssignResources.IncWhere(a => a.AssignTo == userId);
            return View(projectDetails);#1#
        }*/
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetProjectDetails()
        {
            var assignedProject = db.AssignResources.Include(p=>p.Project).Include(u=>u.User).Where(a=>a.UserId==userId).ToList();
            ViewBag.assignedProject = assignedProject;
                
            return View();
        }
    }
}
