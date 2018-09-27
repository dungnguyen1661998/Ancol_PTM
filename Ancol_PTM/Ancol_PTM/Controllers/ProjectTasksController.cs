﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ancol_PTM.libDatabase;

namespace Ancol_PTM.Controllers
{
    public class ProjectTasksController : Controller
    {
        private libEntities db = new libEntities();

        // GET: ProjectTasks
        public ActionResult Index()
        {
            var projectTasks = db.ProjectTasks.Include(p => p.Employee).Include(p => p.TaskStatu).Include(p => p.UserStory);
            return View(projectTasks.ToList());
        }

        // GET: ProjectTasks/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public ActionResult Create()
        {
            ViewBag.Employeeid = new SelectList(db.Employees, "Id", "FirstName");
            ViewBag.TaskStatusid = new SelectList(db.TaskStatus, "Id", "Status");
            ViewBag.UserStoryid = new SelectList(db.UserStories, "Id", "Story");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Employeeid,TaskStartDate,TaskEndDate,TaskStatusid,UserStoryid,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                projectTask.Id = Guid.NewGuid();
                db.ProjectTasks.Add(projectTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employeeid = new SelectList(db.Employees, "Id", "FirstName", projectTask.Employeeid);
            ViewBag.TaskStatusid = new SelectList(db.TaskStatus, "Id", "Status", projectTask.TaskStatusid);
            ViewBag.UserStoryid = new SelectList(db.UserStories, "Id", "Story", projectTask.UserStoryid);
            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employeeid = new SelectList(db.Employees, "Id", "FirstName", projectTask.Employeeid);
            ViewBag.TaskStatusid = new SelectList(db.TaskStatus, "Id", "Status", projectTask.TaskStatusid);
            ViewBag.UserStoryid = new SelectList(db.UserStories, "Id", "Story", projectTask.UserStoryid);
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employeeid,TaskStartDate,TaskEndDate,TaskStatusid,UserStoryid,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employeeid = new SelectList(db.Employees, "Id", "FirstName", projectTask.Employeeid);
            ViewBag.TaskStatusid = new SelectList(db.TaskStatus, "Id", "Status", projectTask.TaskStatusid);
            ViewBag.UserStoryid = new SelectList(db.UserStories, "Id", "Story", projectTask.UserStoryid);
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            db.ProjectTasks.Remove(projectTask);
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
