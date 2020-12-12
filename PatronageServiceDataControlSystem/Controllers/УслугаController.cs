using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatronageServiceDataControlSystem;

namespace PatronageServiceDataControlSystem.Controllers
{
    public class УслугаController : Controller
    {
        private PatronageServiceModel db = new PatronageServiceModel();

        public double MaxDiscount(int? id)
        {               
            return db.Услуга.Find(id).Максимальная_скидка;
        }

        // GET: Услуга
        public ActionResult Index()
        {
            return View(db.Услуга.ToList());
        }

        // GET: Услуга/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Услуга услуга = db.Услуга.Find(id);
            if (услуга == null)
            {
                return HttpNotFound();
            }
            return View(услуга);
        }

        // GET: Услуга/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Услуга/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_услуги,Название_услуги,Описание_услуги,Цена_услуги,Максимальная_скидка")] Услуга услуга)
        {
            if (ModelState.IsValid)
            {
                db.Услуга.Add(услуга);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(услуга);
        }

        // GET: Услуга/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Услуга услуга = db.Услуга.Find(id);
            if (услуга == null)
            {
                return HttpNotFound();
            }
            return View(услуга);
        }

        // POST: Услуга/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_услуги,Название_услуги,Описание_услуги,Цена_услуги,Максимальная_скидка")] Услуга услуга)
        {
            if (ModelState.IsValid)
            {
                db.Entry(услуга).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(услуга);
        }

        public ActionResult Filter(string CostMin, string CostMax, string DiscountMin, string DiscountMax)
        {
            decimal systemCostMin, systemCostMax;
            double systemDiscountMin, systemDiscountMax;
            IQueryable<Услуга> услуга = db.Услуга;
            if (!String.IsNullOrEmpty(CostMin) && decimal.TryParse(CostMin, out systemCostMin))
            {
                услуга = услуга.Where(т => т.Цена_услуги >= systemCostMin);
            }
            if (!String.IsNullOrEmpty(CostMax) && decimal.TryParse(CostMax, out systemCostMax))
            {
                услуга = услуга.Where(т => т.Цена_услуги <= systemCostMax);
            }
            if (!String.IsNullOrEmpty(DiscountMin) && double.TryParse(DiscountMin, out systemDiscountMin))
            {
                услуга = услуга.Where(т => т.Максимальная_скидка >= systemDiscountMin);
            }
            if (!String.IsNullOrEmpty(DiscountMax) && double.TryParse(DiscountMax, out systemDiscountMax))
            {
                услуга = услуга.Where(т => т.Максимальная_скидка <= systemDiscountMax);
            }
            return View(услуга.ToList());
        }

        public ActionResult Search(string ServiceName, string ServiceDescr)
        {
            IQueryable<Услуга> услуга = db.Услуга;
            if (!String.IsNullOrEmpty(ServiceName))
            {
                услуга = услуга.Where(t => t.Название_услуги.Contains(ServiceName));
            }
            if (!String.IsNullOrEmpty(ServiceDescr))
            {
                услуга = услуга.Where(t => t.Описание_услуги.Contains(ServiceDescr));
            }
            return View(услуга.ToList());
        }

        // GET: Услуга/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Услуга услуга = db.Услуга.Find(id);
            if (услуга == null)
            {
                return HttpNotFound();
            }
            return View(услуга);
        }

        // POST: Услуга/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Услуга услуга = db.Услуга.Find(id);
            db.Услуга.Remove(услуга);
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
