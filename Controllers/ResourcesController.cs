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
    public class ResourcesController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public ResourcesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Resources
        [HttpPost]
        public ActionResult Index()
        {
            var customListView = new List<CustomizedView>();
            // getting data from Resource table
            var ResourceList = (from x in db.Resources
                                select x);

            //looping thru each item in list
            foreach (var item in ResourceList)
            {
                var newCustomView = new CustomizedView();
                // checking to see quantity left
                if (item.remainingQuantity > 5)
                {
                    // assigning the respective rows
                    newCustomView.ResourceName = item.resName;
                    newCustomView.ResourceType = db.Resource_Type.Where(x => x.resTypeId == item.resTypeIdFK).Select(x => x.resTypeName).FirstOrDefault();
                    newCustomView.AvailableQuantity = "Sufficient";
                    newCustomView.NumberOfSkills = db.Resource_Allocation.Where(z => z.resIdFK == item.resId).Select(z => z).Count();
                    newCustomView.AllocatedSkills = string.Empty;
                    if (newCustomView.NumberOfSkills == 0)
                    {
                        newCustomView.AllocatedSkills = "Nil";
                    }
                    else
                    {
                        var getAllocatedSkillsList = db.Resource_Allocation.Where(x => x.resIdFK == item.resId).Select(x => x).ToList();
                        foreach (var allocatedSkill in getAllocatedSkillsList)
                        {
                            if (newCustomView.AllocatedSkills == string.Empty)
                            {
                                newCustomView.AllocatedSkills = db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault();
                            }
                            else
                            {
                                newCustomView.AllocatedSkills += $", {db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault()}";
                            }
                        }
                    }

                }
                else if (item.remainingQuantity > 1 && item.remainingQuantity <= 5)
                {
                    newCustomView.ResourceName = item.resName;
                    newCustomView.ResourceType = db.Resource_Type.Where(x => x.resTypeId == item.resTypeIdFK).Select(x => x.resTypeName).FirstOrDefault();
                    newCustomView.AvailableQuantity = "Low Stock";
                    newCustomView.NumberOfSkills = db.Resource_Allocation.Where(z => z.resIdFK == item.resId).Select(z => z).Count();
                    newCustomView.AllocatedSkills = string.Empty;
                    if (newCustomView.NumberOfSkills == 0)
                    {
                        newCustomView.AllocatedSkills = "Nil";
                    }
                    else
                    {
                        var getAllocatedSkillsList = db.Resource_Allocation.Where(x => x.resIdFK == item.resId).Select(x => x).ToList();
                        foreach (var allocatedSkill in getAllocatedSkillsList)
                        {
                            if (newCustomView.AllocatedSkills == string.Empty)
                            {
                                newCustomView.AllocatedSkills = db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault();
                            }
                            else
                            {
                                newCustomView.AllocatedSkills += $", {db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault()}";
                            }
                        }
                    }
                }
                else
                {
                    newCustomView.ResourceName = item.resName;
                    newCustomView.ResourceType = db.Resource_Type.Where(x => x.resTypeId == item.resTypeIdFK).Select(x => x.resTypeName).FirstOrDefault();
                    newCustomView.AvailableQuantity = "Not Available";
                    newCustomView.NumberOfSkills = db.Resource_Allocation.Where(z => z.resIdFK == item.resId).Select(z => z).Count();
                    newCustomView.AllocatedSkills = string.Empty;
                    if (newCustomView.NumberOfSkills == 0)
                    {
                        newCustomView.AllocatedSkills = "Nil";
                    }
                    else
                    {
                        var getAllocatedSkillsList = db.Resource_Allocation.Where(x => x.resIdFK == item.resId).Select(x => x).ToList();
                        foreach (var allocatedSkill in getAllocatedSkillsList)
                        {
                            if (newCustomView.AllocatedSkills == string.Empty)
                            {
                                newCustomView.AllocatedSkills = db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault();
                            }
                            else
                            {
                                newCustomView.AllocatedSkills += $", {db.Skills.Where(x => x.skillId == allocatedSkill.skillIdFK).Select(x => x.skillName).FirstOrDefault()}";
                            }
                        }
                    }
                }

                customListView.Add(newCustomView);
            }
            return new JsonResult { Data = customListView };

        }





















        // GET: Resources/Details/5
        [HttpPost]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

  

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.resTypeIdFK = new SelectList(db.Resource_Type, "resTypeId", "resTypeName", resource.resTypeIdFK);
            return View(resource);
        }

  
        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "resId,resName,resTypeIdFK,remainingQuantity")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.resTypeIdFK = new SelectList(db.Resource_Type, "resTypeId", "resTypeName", resource.resTypeIdFK);
            return View(resource);
        }



        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string ResourceName)
        {
            Resource resource = db.Resources.Where(x => x.resName == ResourceName).Select(x => x).FirstOrDefault();
            var allocation = db.Resource_Allocation.Where(x => x.resIdFK == resource.resId).Select(x => x);
            db.Resources.Remove(resource);
            db.SaveChanges();
            foreach (var item in allocation)
            {
                db.Resource_Allocation.Remove(item);
                db.SaveChanges();
            }
            return Json("Resource has been successfully deleted!");
        }

        public ActionResult GetNewID()
        {
            var newID = db.Resources.OrderByDescending(x => x.resId).Select(x => x.resId).FirstOrDefault() + 1;
            return Json(newID);
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

    public class CustomizedView
    {
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public int NumberOfSkills { get; set; }
        public string AllocatedSkills { get; set; }
        public string AvailableQuantity { get; set; }
    }
}
