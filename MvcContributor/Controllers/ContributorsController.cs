using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcContributor.Models;
using PagedList;

namespace MvcContributor.Controllers
{
    public class ContributorsController : Controller
    {
        private ContributorDBContext db = new ContributorDBContext();

        // GET: Contributors
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var contributors = from c in db.Contributors
                         select c;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                contributors = contributors.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    contributors = contributors.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    contributors = contributors.OrderBy(s => s.BirthDate);
                    break;
                case "date_desc":
                    contributors = contributors.OrderByDescending(s => s.BirthDate);
                    break;
                default:
                    contributors = contributors.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(contributors.ToPagedList(pageNumber,pageSize));
        }

        [HttpPost]
        [ActionName("Upload")]
        public ActionResult Upload([Bind(Include = "ImagePath")] Contributor contributor, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string ext = Path.GetExtension(file.FileName);
                    string filename = Guid.NewGuid().ToString() + ext;
                    //contributor.ImagePath = Url.Content(Path.Combine(Server.MapPath("~/Content/Images"), filename));
                    
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), filename);
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                    //If updating Edit view
                    if (contributor.ID != 0)
                    {
                        var cont = db.Contributors.FirstOrDefault(x => x.ID == contributor.ID);
                        if (cont == null) throw new Exception("Invalid id: " + contributor.ID);
                        cont.ImagePath = "~/Content/Images/" + filename;
                        //db.Contributors.Attach(contributor);
                        //db.Entry(contributor).Property(x => x.ImagePath).IsModified = true;
                        db.SaveChanges();
                    }
                    //else
                    //{
                    //    var cont = db.Contributors.FirstOrDefault(x => x.ID == 0);
                    //    cont.ImagePath = "~/Content/Images/" + filename;
                    //    db.SaveChanges();
                    //    return View("Create");
                    //}
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("Edit",contributor);
        }
        // GET: Contributors/Details/5
        public ActionResult Details(string searchString)
        {
            var contributors = from c in db.Contributors
                               select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                contributors = contributors.Where(s => s.Name.Contains(searchString));
            }

            return View(contributors);
        }

        // GET: Contributors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contributors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,BirthDate,BirthPlace,Biography,BirthName")] Contributor contributor, string SubmitButton, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Contributors.Add(contributor);
                db.SaveChanges();
                switch (SubmitButton)
                {
                    case "Create":
                        return RedirectToAction("Index");
                    case "Upload":
                        return (Upload(contributor, file));
                    default:
                        return View();
                }
            }
            return View(contributor);
            
        }

        
        public ActionResult CreateFromTMDB(/*[Bind(Include = "Name,BirthDate,BirthPlace,Biography,BirthName,ImagePath")] Contributor contributor*/)
        {
            
            Contributor contributor = TempData["newContributor"] as Contributor;
            if (ModelState.IsValid)
            {
                db.Contributors.Add(contributor);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

                // GET: Contributors/Edit/5
                public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contributor contributor = db.Contributors.Find(id);
            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }

        // POST: Contributors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BirthDate,BirthPlace,Biography,BirthName,ImagePath")] Contributor contributor, string SubmitButton, HttpPostedFileBase file)
        {
            switch(SubmitButton)
            {
                case "Save":

                    return (Save(contributor));
                case "Upload":

                    return (Upload(contributor, file));
                default:
                    return View();
            }
           
        }
        private ActionResult Save( Contributor contributor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contributor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contributor);

        }
        // GET: Contributors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contributor contributor = db.Contributors.Find(id);
            if (contributor == null)
            {
                return HttpNotFound();
            }
            return View(contributor);
        }

        // POST: Contributors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contributor contributor = db.Contributors.Find(id);
            db.Contributors.Remove(contributor);
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
