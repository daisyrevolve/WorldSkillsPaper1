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
    public class Resource_TypeController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public Resource_TypeController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }
        // GET: Resource_Type
        [HttpPost]
        public ActionResult Index()
        {
            return new JsonResult { Data = db.Resource_Type.ToList() };
        }

        // GET: Resource_Type/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource_Type resource_Type = db.Resource_Type.Find(id);
            if (resource_Type == null)
            {
                return HttpNotFound();
            }
            return View(resource_Type);
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
