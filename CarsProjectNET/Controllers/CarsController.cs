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
    public class CarsController : Controller
    {
        private CarsProjectdbEntities db = new CarsProjectdbEntities();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.MarkModel).Include(c => c.Mark).Include(c => c.Seller).OrderByDescending(x=> x.CreatedDate);
            return View(cars.ToList());
        }

        public ActionResult Display()
        {
            var cars = db.Cars.Include(c => c.MarkModel).Include(c => c.Mark).Include(c => c.Seller).OrderByDescending(x => x.CreatedDate);
            return View(cars.ToList());
        }


        // GET: Cars/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.MarkModelId = new SelectList(db.MarkModels, "Id", "MarkModelName");
            ViewBag.MarkId = new SelectList(db.Marks, "Id", "MarkName");
            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "SellerName");
            return View(new Car());
        }

        // POST: Cars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,MarkId,SellerId,MarkModelId,ListPrice,Year,ExteriorColor,InteriorColor,Doors,Fuel,Passengers,Transmission,Condition,Picture,Enabled,Description")] Car car, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid().ToString();
                car.CreatedDate = DateTime.Now;

                if (file != null)
                {
                    string pictureUrl = System.IO.Path.GetFileName(file.FileName);
                    string pathUrl = System.IO.Path.Combine(Server.MapPath("/Public/Cars"), pictureUrl);

                    file.SaveAs(pathUrl);
                    car.Picture = pictureUrl;
                }

                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarkModelId = new SelectList(db.MarkModels, "Id", "MarkModelName", car.MarkModelId);
            ViewBag.MarkId = new SelectList(db.Marks, "Id", "MarkName", car.MarkId);
            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "SellerName", car.SellerId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarkModelId = new SelectList(db.MarkModels, "Id", "MarkModelName", car.MarkModelId);
            ViewBag.MarkId = new SelectList(db.Marks, "Id", "MarkName", car.MarkId);
            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "SellerName", car.SellerId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MarkId,SellerId,MarkModelId,ListPrice,Year,ExteriorColor,InteriorColor,Doors,Fuel,Passengers,Transmission,Condition,Picture,Enabled,Description")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.CreatedDate = DateTime.Now;
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarkModelId = new SelectList(db.MarkModels, "Id", "MarkModelName", car.MarkModelId);
            ViewBag.MarkId = new SelectList(db.Marks, "Id", "MarkName", car.MarkId);
            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "SellerName", car.SellerId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
