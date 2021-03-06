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
    public class UserStoriesController : Controller
    {
        private libEntities db = new libEntities();

        // GET: UserStories
        public ActionResult Index()
        {
            var userStories = db.UserStories.Include(u => u.Project);
            return View(userStories.ToList());
        }

       
        // GET: UserStories/Create
        public ActionResult Create()
        {
            ViewBag.Projectid = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: UserStories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Story,Projectid,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                userStory.Id = Guid.NewGuid();
                db.UserStories.Add(userStory);
                InsertAuditFields(userStory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Projectid = new SelectList(db.Projects, "Id", "Name", userStory.Projectid);
            return View(userStory);
        }

        // GET: UserStories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStory userStory = db.UserStories.Find(id);
            if (userStory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Projectid = new SelectList(db.Projects, "Id", "Name", userStory.Projectid);
            return View(userStory);
        }

        // POST: UserStories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Story,Projectid,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userStory).State = EntityState.Modified;
                UpdateAuditFields(userStory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Projectid = new SelectList(db.Projects, "Id", "Name", userStory.Projectid);
            return View(userStory);
        }

    
        public ActionResult Delete(Guid? id)
        {
            UserStory userStory = db.UserStories.Find(id);
            db.UserStories.Remove(userStory);
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
        private void InsertAuditFields(UserStory userStory)
        {
            userStory.IsDeleted = false;
            userStory.InsAt = DateTime.Now;
            userStory.InsBy = "Admin";
            userStory.UpdAt = DateTime.Now;
            userStory.UpdBy = "Admin";
        }
        private void UpdateAuditFields(UserStory userStory)
        {
            userStory.UpdAt = DateTime.Now;
            userStory.UpdBy = "Admin";
        }
    }
}
