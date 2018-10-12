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
    public class ProjectsController : Controller
    {
        private libEntities db = new libEntities();

        // GET: Projects
        public ActionResult Index(String SearchName, DateTime? SearchStartDate, DateTime? SearchEndDate)
        {

            var querry = from p in db.Projects
                         where (bool)!p.IsDeleted
                         select p;
            //var list = db.Employees.Where(p => !Convert.ToBoolean( p.IsDeleted)).Select(p => p).ToList();
            if (!string.IsNullOrEmpty(SearchName))
            {
                querry = from p in querry
                         where (p.Name.Contains(SearchName))
                         select p;
            }
            if ((SearchStartDate != null))
            {
                querry = from p in querry
                         where ((p.StartDate >= SearchStartDate))
                         select p;
            }
            if ((SearchEndDate != null))
            {
                querry = from p in querry
                         where ((p.EndDate <= SearchEndDate))
                         select p;
            }

            return View(querry.ToList());
        }



        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StartDate,EndDate,ClientName,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Id = Guid.NewGuid();
                db.Projects.Add(project);
                InsertAuditFields(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(Guid? id)
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

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StartDate,EndDate,ClientName,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                UpdateAuditFields(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        
        public ActionResult Delete(Guid? id)
        {
            Project project = db.Projects.Find(id);
            project.IsDeleted = true;
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
        private void InsertAuditFields(Project project)
        {
            project.IsDeleted = false;
            project.InsAt = DateTime.Now;
            project.InsBy = "Admin";
            project.UpdAt = DateTime.Now;
            project.UpdBy = "Admin";
        }
        private void UpdateAuditFields(Project project)
        {
            project.UpdAt = DateTime.Now;
            project.UpdBy = "Admin";
        }
    }
}
