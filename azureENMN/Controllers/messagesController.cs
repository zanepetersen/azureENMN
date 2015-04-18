using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using azureENMN.Models;

namespace azureENMN.Controllers
{
    public class messagesController : Controller
    {
        private enmndbEntities db = new enmndbEntities();

        // GET: messages
        public ActionResult Index(int ThreadID)
        {
            var messages = db.messages.Include(m => m.person).Include(m => m.messagethread).Where(m => m.MessageThreadID == ThreadID).OrderBy(m => m.OrderNo);
            return View(messages.ToList());
        }

        // GET: messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            message message = db.messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: messages/Create
        public ActionResult Create()
        {
            ViewBag.SenderID = new SelectList(db.people, "PersonID", "FirstName");
            ViewBag.MessageThreadID = new SelectList(db.messagethreads, "MessageThreadID", "MessageThreadID");
            return PartialView("_CreatePartial", new azureENMN.Models.message());
        }

        // POST: messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MessageID,MessageThreadID,DateTime,OrderNo,Text,SenderID")] message message, DateTime dateTime, string senderID, string threadID)
        {
            int temp = 0;
            message.DateTime = dateTime;
            temp = Convert.ToInt32(senderID);
            message.SenderID = temp;
            temp = Convert.ToInt32(threadID);
            message.MessageThreadID = temp;
            enmndbEntities dbc = new enmndbEntities();
            message.OrderNo = dbc.messages.Where(m => m.MessageThreadID == temp).Count();
            if (ModelState.IsValid)
            {
                db.messages.Add(message);
                db.SaveChanges();
                int t = dbc.messagethreads.Where(m => m.MessageThreadID == message.MessageThreadID).First().MotherID;
                String id = dbc.people.Where(m => m.PersonID == t).First().GCMConnectionString;
                sendGCM(id, message.MessageID);
                return RedirectToAction("Index", new { @ThreadID = threadID });
            }

            ViewBag.SenderID = new SelectList(db.people, "PersonID", "FirstName", message.SenderID);
            ViewBag.MessageThreadID = new SelectList(db.messagethreads, "MessageThreadID", "MessageThreadID", message.MessageThreadID);
            return Redirect("Messages?" + threadID);
        }

        // GET: messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            message message = db.messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.SenderID = new SelectList(db.people, "PersonID", "FirstName", message.SenderID);
            ViewBag.MessageThreadID = new SelectList(db.messagethreads, "MessageThreadID", "MessageThreadID", message.MessageThreadID);
            return View(message);
        }

        // POST: messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageID,MessageThreadID,DateTime,OrderNo,Text,SenderID")] message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SenderID = new SelectList(db.people, "PersonID", "FirstName", message.SenderID);
            ViewBag.MessageThreadID = new SelectList(db.messagethreads, "MessageThreadID", "MessageThreadID", message.MessageThreadID);
            return View(message);
        }

        // GET: messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            message message = db.messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            message message = db.messages.Find(id);
            db.messages.Remove(message);
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

        void sendGCM(string regId, int messageId)
        {
            var applicationID = "AIzaSyCb7ulAlp7e7lQQ_gjtiZvIB0FpjSm0IU8";


            var SENDER_ID = "763683898982";
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + messageId + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "";
            Console.WriteLine(postData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }
    }
}
