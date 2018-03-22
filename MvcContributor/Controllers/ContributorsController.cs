using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcContributor.Models;

namespace MvcContributor.Controllers
{
    public class ContributorsController : Controller
    {
        private ContributorDBContext db = new ContributorDBContext();

        // GET: Contributors
        public ActionResult Index(string searchString)
        {
            var contributors = from c in db.Contributors
                         select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                contributors = contributors.Where(s => s.Name.Contains(searchString));
            }

            return View(contributors);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("Edit");
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
        public ActionResult Create([Bind(Include = "ID,Name,BirthDate,BirthPlace,Biography,ImageURL")] Contributor contributor)
        {
            if (ModelState.IsValid)
            {
                db.Contributors.Add(contributor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contributor);
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
        public ActionResult Edit([Bind(Include = "ID,Name,BirthDate,BirthPlace,Biography")] Contributor contributor)
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
