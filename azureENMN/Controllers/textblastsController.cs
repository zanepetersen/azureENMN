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
    public class textblastsController : Controller
    {
        private enmndbEntities db = new enmndbEntities();

        // GET: textblasts
        public ActionResult Index()
        {
            var textblasts = db.textblasts.Include(t => t.group).Include(t => t.person);
            return View(textblasts.ToList());
        }

        // GET: textblasts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            textblast textblast = db.textblasts.Find(id);
            if (textblast == null)
            {
                return HttpNotFound();
            }
            return View(textblast);
        }

        // GET: textblasts/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.groups, "GroupID", "Name");
            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName");
            return View();
        }

        // POST: textblasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TextBlastID,Title,Creator,GroupID,DateSent")] textblast textblast)
        {
            textblast.Creator = Convert.ToInt32(Session["LoggedUserID"].ToString());
            if (ModelState.IsValid)
            {
                db.textblasts.Add(textblast);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.groups, "GroupID", "Name", textblast.GroupID);
            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName", textblast.Creator);
            return View(textblast);
        }

        // GET: textblasts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            textblast textblast = db.textblasts.Find(id);
            if (textblast == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.groups, "GroupID", "Name", textblast.GroupID);
            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName", textblast.Creator);
            return View(textblast);
        }

        // POST: textblasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TextBlastID,Title,Creator,GroupID,DateSent")] textblast textblast)
        {
            if (ModelState.IsValid)
            {
                db.Entry(textblast).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.groups, "GroupID", "Name", textblast.GroupID);
            ViewBag.Creator = new SelectList(db.people, "PersonID", "FirstName", textblast.Creator);
            return View(textblast);
        }

        // GET: textblasts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            textblast textblast = db.textblasts.Find(id);
            if (textblast == null)
            {
                return HttpNotFound();
            }
            return View(textblast);
        }

        // POST: textblasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            textblast textblast = db.textblasts.Find(id);
            db.textblasts.Remove(textblast);
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
