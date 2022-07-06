using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarsProjectNET;

namespace CarsProjectNET.Controllers
{
    public class MarkModelsController : Controller
    {
        private CarsProjectdbEntities db = new CarsProjectdbEntities();

        // GET: MarkModels
        public ActionResult Index()
        {
            return View(db.MarkModels.ToList());
        }

        // GET: MarkModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkModel markModel = db.MarkModels.Find(id);
            if (markModel == null)
            {
                return HttpNotFound();
            }
            return View(markModel);
        }

        // GET: MarkModels/Create
        public ActionResult Create()
        {
            return View(new MarkModel());
        }

        // POST: MarkModels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkModelName,Description,Enabled")] MarkModel markModel)
        {
            if (ModelState.IsValid)
            {
                markModel.Id = new Random().Next();
                markModel.CreatedDate = DateTime.Now;
                db.MarkModels.Add(markModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markModel);
        }

        // GET: MarkModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkModel markModel = db.MarkModels.Find(id);
            if (markModel == null)
            {
                return HttpNotFound();
            }
            return View(markModel);
        }

        // POST: MarkModels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MarkModelName,Description,Enabled")] MarkModel markModel)
        {
            if (ModelState.IsValid)
            {

                markModel.CreatedDate = DateTime.Now;
                db.Entry(markModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markModel);
        }

        // GET: MarkModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkModel markModel = db.MarkModels.Find(id);
            if (markModel == null)
            {
                return HttpNotFound();
            }
            return View(markModel);
        }

        // POST: MarkModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarkModel markModel = db.MarkModels.Find(id);
            db.MarkModels.Remove(markModel);
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
