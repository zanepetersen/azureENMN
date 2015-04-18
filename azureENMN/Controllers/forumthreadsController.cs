using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using azureENMN.Models;

namespace azureENMN.Controllers
{
    public class forumthreadsController : Controller
    {
        private enmndbEntities db = new enmndbEntities();

        // GET: forumthreads
        public ActionResult Index()
        {
            var forumthreads = db.forumthreads.Include(f => f.person);
            return View(forumthreads.ToList());
        }

        // GET: forumthreads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forumthread forumthread = db.forumthreads.Find(id);
            if (forumthread == null)
            {
                return HttpNotFound();
            }
            return View(forumthread);
        }

        // GET: forumthreads/Create
        public ActionResult Create()
        {
            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName");
            return View();
        }

        // POST: forumthreads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ForumThreadID,Creator,Title")] forumthread forumthread)
        {
            if (ModelState.IsValid)
            {
                db.forumthreads.Add(forumthread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName", forumthread.Creator);
            return View(forumthread);
        }

        // GET: forumthreads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forumthread forumthread = db.forumthreads.Find(id);
            if (forumthread == null)
            {
                return HttpNotFound();
            }
            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName", forumthread.Creator);
            return View(forumthread);
        }

        // POST: forumthreads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ForumThreadID,Creator,Title")] forumthread forumthread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forumthread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName", forumthread.Creator);
            return View(forumthread);
        }

        // GET: forumthreads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forumthread forumthread = db.forumthreads.Find(id);
            if (forumthread == null)
            {
                return HttpNotFound();
            }
            return View(forumthread);
        }

        // POST: forumthreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            forumthread forumthread = db.forumthreads.Find(id);
            db.forumthreads.Remove(forumthread);
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
