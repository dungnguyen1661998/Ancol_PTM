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
        public ActionResult Index()
        {
            var managerComments = db.ManagerComments.Include(m => m.ProjectTask);
            return View(managerComments.ToList());
        }

        // GET: ManagerComments/Details/5
        public ActionResult Details(Guid? id)
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
            return View(managerComment);
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
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectTaskid = new SelectList(db.ProjectTasks, "Id", "InsBy", managerComment.ProjectTaskid);
            return View(managerComment);
        }

        // GET: ManagerComments/Delete/5
        public ActionResult Delete(Guid? id)
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
            return View(managerComment);
        }

        // POST: ManagerComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
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
    }
}
