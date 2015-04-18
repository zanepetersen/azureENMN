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
    public class messagethreadsController : Controller
    {
        private enmndbEntities db = new enmndbEntities();

        // GET: messagethreads
        public ActionResult Index()
        {
            int loggedID = Convert.ToInt32(Session["LoggedUserID"].ToString());
            var messageThreads = db.messagethreads.Include(m => m.person).Include(m => m.person1).Where(m => m.NurseID == loggedID);
            return View(messageThreads.ToList());
        }

        // GET: messagethreads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            messagethread messagethread = db.messagethreads.Find(id);
            if (messagethread == null)
            {
                return HttpNotFound();
            }
            return View(messagethread);
        }

        // GET: messagethreads/Create
        public ActionResult Create()
        {

            string type = "MOTHER";
            ViewBag.MotherID = new SelectList(db.people.Where(m => m.Type == type), "PersonID", "FirstName");
            ViewBag.NurseID = new SelectList(db.people, "PersonID", "FirstName");
            return View();
        }

        // POST: messagethreads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageThreadID,NurseID,MotherID")] messagethread messagethread)
        {
            messagethread.NurseID = Convert.ToInt32(Session["LoggedUserID"].ToString());
            if (ModelState.IsValid)
            {
                db.messagethreads.Add(messagethread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MotherID = new SelectList(db.people, "PersonID", "FirstName", messagethread.MotherID);
            ViewBag.NurseID = new SelectList(db.people, "PersonID", "FirstName", messagethread.NurseID);
            return View(messagethread);
        }

        // GET: messagethreads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            messagethread messagethread = db.messagethreads.Find(id);
            if (messagethread == null)
            {
                return HttpNotFound();
            }
            ViewBag.MotherID = new SelectList(db.people, "PersonID", "FirstName", messagethread.MotherID);
            ViewBag.NurseID = new SelectList(db.people, "PersonID", "FirstName", messagethread.NurseID);
            return View(messagethread);
        }

        // POST: messagethreads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageThreadID,NurseID,MotherID")] messagethread messagethread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messagethread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MotherID = new SelectList(db.people, "PersonID", "FirstName", messagethread.MotherID);
            ViewBag.NurseID = new SelectList(db.people, "PersonID", "FirstName", messagethread.NurseID);
            return View(messagethread);
        }

        // GET: messagethreads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            messagethread messagethread = db.messagethreads.Find(id);
            if (messagethread == null)
            {
                return HttpNotFound();
            }
            return View(messagethread);
        }

        // POST: messagethreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            messagethread messagethread = db.messagethreads.Find(id);
            db.messagethreads.Remove(messagethread);
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
