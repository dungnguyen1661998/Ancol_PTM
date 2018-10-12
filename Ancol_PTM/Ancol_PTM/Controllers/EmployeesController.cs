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
    public class EmployeesController : Controller
    {
        private libEntities db = new libEntities();

        // GET: Employees
        public ActionResult Index(String SearchName, String SearchPN)
        {
      
            var querry = from p in db.Employees
                         where (bool)!p.IsDeleted
                         select p;
            //var list = db.Employees.Where(p => !Convert.ToBoolean( p.IsDeleted)).Select(p => p).ToList();
            if (!string.IsNullOrEmpty(SearchName))
            {
                 querry = from p in querry
                        where (SearchName.Contains(p.LastName) || p.FirstName.Contains(SearchName) || p.LastName.Contains(SearchName))
                      select p;
            }
            if (!string.IsNullOrEmpty(SearchPN))
            {
                querry = from p in querry
                         where p.ContactNo.Contains(SearchPN)
                         select p;
            }
            return View(querry.ToList());
        }
            
        
        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,ContactNo,Email,Skills,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                db.Employees.Add(employee);
                InsertAuditFields(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,ContactNo,Email,Skills,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                UpdateAuditFields(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        
        public ActionResult Delete(Guid? id)
        {
            Employee employee = db.Employees.Find(id);
            employee.IsDeleted = true;
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
        private void InsertAuditFields(Employee employee)
        {
            employee.IsDeleted = false;
            employee.InsAt = DateTime.Now;
            employee.InsBy = "Admin";
            employee.UpdAt = DateTime.Now;
            employee.UpdBy = "Admin";
        }
        private void UpdateAuditFields(Employee employee)
        {
            employee.UpdAt = DateTime.Now;
            employee.UpdBy = "Admin";

        }
    }
}
