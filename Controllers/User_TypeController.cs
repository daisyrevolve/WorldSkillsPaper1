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
    public class User_TypeController : Controller
    {
        private Session1Entities db = new Session1Entities();

        // POST: User_Type

        public User_TypeController() {
            db.Configuration.LazyLoadingEnabled = false;
        }
        [HttpPost]
        public ActionResult Index()
        {
            return new JsonResult { Data = db.User_Type.ToList() };
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
