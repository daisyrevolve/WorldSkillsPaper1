using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldSkillsPaper1;

namespace WorldSkillsPaper1.Controllers
{
    public class UsersController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public UsersController() {
            db.Configuration.LazyLoadingEnabled = false;

        }

        //POST: Users/Login?username={username}&
        [HttpPost]
        public ActionResult Login(string username, string password) {
            var user = (from x in db.Users where x.userId == username &&
                        x.userPw == password select x).FirstOrDefault();

            if (user != null) {
                return Json(user);

            }
            return Json("user does not exists.");
        }

        // GET: Users/Details/5
        //to edit if needed 
        [HttpPost]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return Json(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(x => x.userId == id).Select(x => x).FirstOrDefault();
            if (user == null)
            {
                return Json(HttpStatusCode.BadRequest);
            }
            return Json(user);
        }

      

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "userId,userName,userPw,userTypeIdFK")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Json("User has been created.");
            }

            return Json("error, user cannot be created. Please check your fields or try again.");
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "userId,userName,userPw,userTypeIdFK")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userTypeIdFK = new SelectList(db.User_Type, "userTypeId", "userTypeName", user.userTypeIdFK);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
