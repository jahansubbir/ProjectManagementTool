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
    public class ProjectsController : Controller
    {
        private HttpContext context = System.Web.HttpContext.Current;
        private HttpCookie cookie;

        private ProjectContext db = new ProjectContext();
        private string userId, designation, path;

        public ProjectsController()
        {

            cookie = context.Request.Cookies["loginCookie"];
            if (cookie != null)
            {
                userId = (cookie["UserId"]).ToString();
                designation = (cookie["Designation"]).ToString();
            }
        }// GET: Projects
        public ActionResult Index()
        {
            if (userId != null && designation.Equals("Project Manager"))
            {


                var projects = db.Projects.Include(p => p.ProjectStatus);
                return View(projects.ToList());
            }
            TempData["message"] = "Unothorized attempt";
            return RedirectToAction("Index", "Home");
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (userId != null && designation.Equals("Project Manager"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Project project = db.Projects.Find(id);
                if (project == null)
                {
                    return HttpNotFound();
                }
                return View(project);
            }
            TempData["message"] = "Unothorized attempt";
            return RedirectToAction("Index", "Home");
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            if (userId != null && designation.Equals("Project Manager"))
            {
                ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Status");
                return View();
            }
            TempData["message"] = "Unothorized attempt";
            return RedirectToAction("Index", "Home");
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CodeName,Description,PossibleStartDate,PossibleEndDate,Duration,UploadedFileName,ProjectStatusId,File")] Project project)
        {
            UploadFile(project);
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                int rowEffected = db.SaveChanges();
                if (rowEffected > 0)
                {
                    project.File.SaveAs(path);
                }

                return RedirectToAction("Index");
            }

            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Status", project.ProjectStatusId);
            return View(project);
        }


        public void UploadFile(Project project)
        {
            string directoryPath = null;
            if (project.File != null)
            {
                directoryPath = Server.MapPath("~/Attachments/" + project.CodeName + "/");
                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }
                string fileName = project.File.FileName;
                path = directoryPath + fileName;
                project.UploadedFileName = fileName;

            }
        }
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (userId != null && designation.Equals("Project Manager"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Status", project.ProjectStatusId);
            return View(project);
            }
            TempData["message"] = "Unothorized attempt";
            return RedirectToAction("Index", "Home");
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CodeName,Description,PossibleStartDate,PossibleEndDate,Duration,UploadedFileName,ProjectStatusId")] Project project)
        {
            UploadFile(project);
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                int rowEffected = db.SaveChanges();
                if (rowEffected > 0)
                {
                    project.File.SaveAs(path);
                }
                return RedirectToAction("Index");
            }
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Status", project.ProjectStatusId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (userId != null && designation.Equals("Project Manager"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        TempData["message"] = "Unothorized attempt";
            return RedirectToAction("Index", "Home");
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
