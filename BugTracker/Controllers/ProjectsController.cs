﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using BugTracker.Helpers;
using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectManagerHelper projectHelper = new ProjectManagerHelper();

        [Authorize(Roles = "Admin, Project Manager,Developer, Submitter")]
        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
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
            
            var allProjectManagers = roleHelper.UsersInRole("Project Manager");
            var currentProjectManagers = projectHelper.UsersInRoleOnProject(project.Id, "ProjectManager");
            ViewBag.ProjectManagers = new MultiSelectList(allProjectManagers, "Id", "FullName", currentProjectManagers);

            var allSubmitters = roleHelper.UsersInRole("Submitter");
            var currentSubmitters = projectHelper.UsersInRoleOnProject(project.Id, "Submitters");
            ViewBag.Submitters = new MultiSelectList(allSubmitters, "Id", "FullName", currentSubmitters);

            var allDevelopers = roleHelper.UsersInRole("Developer");
            var currentDeveoplers= projectHelper.UsersInRoleOnProject(project.Id, "Developers");
            ViewBag.Developers = new MultiSelectList(allDevelopers, "Id", "FullName", currentDeveoplers);

            

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Created = DateTime.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);

        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, Project Manager,Developer, Submitter")]
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }
            if(DecisionHelper.ProjectIsEditableByUser(project))
            {
                return View(project);
            }

            return RedirectToAction("AccessViolation, Admin");
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Delete(int? id)
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
