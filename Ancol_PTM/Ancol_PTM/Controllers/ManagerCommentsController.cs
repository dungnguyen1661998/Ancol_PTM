using System;
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
    public class ManagerCommentsController : Controller
    {
        private libEntities db = new libEntities();

        // GET: ManagerComments
        public ActionResult Index(String SearchName, Guid? SearchProjectTaskid)
        {
            ViewBag.ProjectTaskid = new SelectList(db.ProjectTasks, "Id", "Id");
            
            var querry = from p in db.ManagerComments
                         where (bool)!p.IsDeleted
                         select p;
            //var list = db.Employees.Where(p => !Convert.ToBoolean( p.IsDeleted)).Select(p => p).ToList();
            if (!string.IsNullOrEmpty(SearchName))
            {
                querry = from p in querry
                         where (SearchName.Contains(p.LastName) || p.FirstName.Contains(SearchName) || p.LastName.Contains(SearchName))
                         select p;
            }
           
            return View(querry.ToList());
        }

        
        // GET: ManagerComments/Create
        public ActionResult Create()
        {
            ViewBag.ProjectTaskid = new SelectList(db.ProjectTasks, "Id", "InsBy");
            return View();
        }

        // POST: ManagerComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,ProjectTaskid,Comments,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] ManagerComment managerComment)
        {
            if (ModelState.IsValid)
            {
                managerComment.Id = Guid.NewGuid();
                db.ManagerComments.Add(managerComment);
                InsertAuditFields(managerComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectTaskid = new SelectList(db.ProjectTasks, "Id", "InsBy", managerComment.ProjectTaskid);
            return View(managerComment);
        }

        // GET: ManagerComments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerComment managerComment = db.ManagerComments.Find(id);
            if (managerComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectTaskid = new SelectList(db.ProjectTasks, "Id", "InsBy", managerComment.ProjectTaskid);
            return View(managerComment);
        }

        // POST: ManagerComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,ProjectTaskid,Comments,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] ManagerComment managerComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managerComment).State = EntityState.Modified;
                UpdateAuditFields(managerComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectTaskid = new SelectList(db.ProjectTasks, "Id", "InsBy", managerComment.ProjectTaskid);
            return View(managerComment);
        }

       
        public ActionResult Delete(Guid? id)
        {
            ManagerComment managerComment = db.ManagerComments.Find(id);
            db.ManagerComments.Remove(managerComment);
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
        private void InsertAuditFields(ManagerComment manager)
        {
            manager.IsDeleted = false;
            manager.InsAt = DateTime.Now;
            manager.InsBy = "Admin";
            manager.UpdAt = DateTime.Now;
            manager.UpdBy = "Admin";
        }
        private void UpdateAuditFields(ManagerComment manager)
        {
            manager.UpdAt = DateTime.Now;
            manager.UpdBy = "Admin";
        }
    }
}
