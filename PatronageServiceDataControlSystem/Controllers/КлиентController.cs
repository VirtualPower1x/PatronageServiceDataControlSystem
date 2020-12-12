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
    public class КлиентController : Controller
    {
        private PatronageServiceModel db = new PatronageServiceModel();

        // GET: Клиент
        public ActionResult Index()
        {
            return View(db.Клиент.ToList());
        }

        // GET: Клиент/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);
            if (клиент == null)
            {
                return HttpNotFound();
            }
            return View(клиент);
        }

        // GET: Клиент/Create
        public ActionResult Create()
        {
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            ViewBag.Пол_клиента = new SelectList(gender);
            return View();            
        }

        // POST: Клиент/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_клиента,Фамилия_клиента,Имя_клиента,Пол_клиента,Дата_рождения_клиента,Адрес_клиента,Телефон_клиента,Заболевание,Категория_инвалидности,Постоянный_уход")] Клиент клиент)
        {
            if (ModelState.IsValid)
            {
                db.Клиент.Add(клиент);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            ViewBag.Пол_клиента = new SelectList(gender, клиент.Пол_клиента);
            return View(клиент);
        }

        // GET: Клиент/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);
            if (клиент == null)
            {
                return HttpNotFound();
            }
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            ViewBag.Пол_клиента = new SelectList(gender, клиент.Пол_клиента);
            return View(клиент);
        }

        // POST: Клиент/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_клиента,Фамилия_клиента,Имя_клиента,Пол_клиента,Дата_рождения_клиента,Адрес_клиента,Телефон_клиента,Заболевание,Категория_инвалидности,Постоянный_уход")] Клиент клиент)
        {
            if (ModelState.IsValid)
            {
                db.Entry(клиент).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            ViewBag.Пол_клиента = new SelectList(gender, клиент.Пол_клиента);
            return View(клиент);
        }



        public ActionResult Filter(int? ServicesID, string Gender, string DateOfBirthMin)
        {
            ViewBag.ServicesID = new SelectList(db.Услуга, "ID_услуги", "Название_услуги");
            List<string> gender = new List<string>() { "(Показать все)", "Мужской", "Женский" };

            ViewBag.Gender = new SelectList(gender);

            DateTime systemDateOfBirthMin;
            IQueryable<Клиент> клиент = db.Клиент;
            
            IQueryable<Талон_ухода_за_клиентом> талон = db.Талон_ухода_за_клиентом;
            if (ServicesID != null)
            {
                IQueryable<Клиент> targetClients = клиент.Where(t => t.ID_клиента == -100);
                талон = талон.Where(т => т.ID_услуги == ServicesID);
                List<int> TargetClientIDs = new List<int>();
                foreach (Талон_ухода_за_клиентом item in талон)
                {
                    targetClients = targetClients.Union(клиент.Where(t => t.ID_клиента == item.ID_клиента));
                }
                клиент = targetClients;
            }
            if (!String.IsNullOrEmpty(DateOfBirthMin) && DateTime.TryParse(DateOfBirthMin, out systemDateOfBirthMin))
            {
                клиент = клиент.Where(т => т.Дата_рождения_клиента >= systemDateOfBirthMin);
            }
            if (Gender != null && Gender != "(Показать все)")
            {
                клиент = клиент.Where(т => т.Пол_клиента == Gender);
            }            
            return View(клиент.ToList());
        }

        public ActionResult Search(string ClientFirstName, string ClientLastName, string ClientAddress, string ClientDisease)
        {
            IQueryable<Клиент> клиент = db.Клиент;

            if (!String.IsNullOrEmpty(ClientFirstName))
            {
                клиент = клиент.Where(t => t.Фамилия_клиента.Contains(ClientFirstName));
            }
            if (!String.IsNullOrEmpty(ClientLastName))
            {
                клиент = клиент.Where(t => t.Имя_клиента.Contains(ClientLastName));
            }
            if (!String.IsNullOrEmpty(ClientAddress))
            {
                клиент = клиент.Where(t => t.Адрес_клиента.Contains(ClientAddress));
            }
            if (!String.IsNullOrEmpty(ClientDisease))
            {
                клиент = клиент.Where(t => t.Заболевание.Contains(ClientDisease));
            }
            return View(клиент.ToList());
        }

        // GET: Клиент/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);
            if (клиент == null)
            {
                return HttpNotFound();
            }
            return View(клиент);
        }

        // POST: Клиент/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Клиент клиент = db.Клиент.Find(id);
            db.Клиент.Remove(клиент);
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
