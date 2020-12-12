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
    public class ExtraServiceHeadException : DataException
    {
        public ExtraServiceHeadException(string message) : base(message)
        {

        }
    }
    public class СотрудникController : Controller
    {
        private PatronageServiceModel db = new PatronageServiceModel();

        // GET: Сотрудник
        public ActionResult Index()
        {
            return View(db.Сотрудник.ToList());
        }

        // GET: Сотрудник/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            if (сотрудник == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник);
        }

        // GET: Сотрудник/Create
        public ActionResult Create()
        {
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            IEnumerable<string> position = new List<string>() { "Глава службы", "Врач", "Медсестра", "Няня", "Психолог", "Добровольный помощник" };
            ViewBag.Пол = new SelectList(gender);
            ViewBag.Должность = new SelectList(position);
            return View();
        }

        // POST: Сотрудник/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_сотрудника,Фамилия_сотрудника,Имя_сотрудника,Пол,Дата_рождения,Домашний_адрес_сотрудника,Телефон_сотрудника,Дата_приема_на_работу,Должность,Образование,Стаж_работы,Заработная_плата,Специализация")] Сотрудник сотрудник)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (сотрудник.Должность == "Глава службы" && db.Сотрудник.Count(т => т.Должность == "Глава службы") >= 1)
                    {
                        ExtraServiceHeadException ex = new ExtraServiceHeadException("Невозможно добавить более одного главы службы.");
                        throw ex;
                    }
                    else
                    {
                        db.Сотрудник.Add(сотрудник);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }                
                catch (ExtraServiceHeadException ex)
                {
                    ViewData["ExtraServiceHeadErrorMessage"] = ex.Message;
                }
            }
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            IEnumerable<string> position = new List<string>() { "Глава службы", "Врач", "Медсестра", "Няня", "Психолог", "Добровольный помощник" };
            ViewBag.Пол = new SelectList(gender, сотрудник.Пол);
            ViewBag.Должность = new SelectList(position, сотрудник.Должность);
            return View(сотрудник);
        }

        // GET: Сотрудник/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            if (сотрудник == null)
            {
                return HttpNotFound();
            }
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            IEnumerable<string> position = new List<string>() { "Глава службы", "Врач", "Медсестра", "Няня", "Психолог", "Добровольный помощник" };
            ViewBag.Пол = new SelectList(gender, сотрудник.Пол);
            ViewBag.Должность = new SelectList(position, сотрудник.Должность);
            return View(сотрудник);
        }

        // POST: Сотрудник/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_сотрудника,Фамилия_сотрудника,Имя_сотрудника,Пол,Дата_рождения,Домашний_адрес_сотрудника,Телефон_сотрудника,Дата_приема_на_работу,Должность,Образование,Стаж_работы,Заработная_плата,Специализация")] Сотрудник сотрудник)
        {            
            if (ModelState.IsValid)
            {
                try
                {
                    if (сотрудник.Должность == "Глава службы" && db.Сотрудник.Count(т => т.Должность == "Глава службы") >= 1)
                    {
                        ExtraServiceHeadException ex = new ExtraServiceHeadException("Невозможно добавить более одного главы службы.");
                        throw ex;
                    }
                    else
                    {
                        db.Entry(сотрудник).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (ExtraServiceHeadException ex)
                {
                    ViewData["ExtraServiceHeadErrorMessage"] = ex.Message;
                }
            }
            IEnumerable<string> gender = new List<string>() { "Мужской", "Женский" };
            IEnumerable<string> position = new List<string>() { "Глава службы", "Врач", "Медсестра", "Няня", "Психолог", "Добровольный помощник" };
            ViewBag.Пол = new SelectList(gender, сотрудник.Пол);
            ViewBag.Должность = new SelectList(position, сотрудник.Должность);
            return View(сотрудник);
        }

        public ActionResult Filter(string Gender, string DateOfBirthMin, string EmployDateMin, string Position, string Experience)
        {
            List<string> gender = new List<string>() { "(Показать все)", "Мужской", "Женский" };
            ViewBag.Gender = new SelectList(gender);
            List<string> position = new List<string>() { "(Показать все)", "Глава службы", "Врач", "Медсестра", "Няня", "Психолог", "Добровольный помощник" };
            ViewBag.Position = new SelectList(position);

            DateTime systemDateOfBirthMin, systemEmployDateMin;
            int systemExp;
            IQueryable<Сотрудник> сотрудник = db.Сотрудник;
            if (Gender != null && Gender != "(Показать все)")
            {
                сотрудник = сотрудник.Where(т => т.Пол == Gender);
            }
            if (!String.IsNullOrEmpty(DateOfBirthMin) && DateTime.TryParse(DateOfBirthMin, out systemDateOfBirthMin))
            {
                сотрудник = сотрудник.Where(т => т.Дата_рождения >= systemDateOfBirthMin);
            }
            if (!String.IsNullOrEmpty(EmployDateMin) && DateTime.TryParse(EmployDateMin, out systemEmployDateMin))
            {
                сотрудник = сотрудник.Where(т => т.Дата_приема_на_работу >= systemEmployDateMin);
            }
            if (Position != null && Position != "(Показать все)")
            {
                сотрудник = сотрудник.Where(т => т.Должность == Position);
            }
            if (!String.IsNullOrEmpty(Experience) && int.TryParse(Experience, out systemExp))
            {
                сотрудник = сотрудник.Where(т => т.Стаж_работы >= systemExp);
            }
            return View(сотрудник.ToList());
        }

        public ActionResult Search(string EmployeeFirstName, string EmployeeLastName, string EmployeeAddress, string Education, string Specialization)
        {
            IQueryable<Сотрудник> сотрудник = db.Сотрудник;

            if (!String.IsNullOrEmpty(EmployeeFirstName))
            {
                сотрудник = сотрудник.Where(t => t.Фамилия_сотрудника.Contains(EmployeeFirstName));
            }
            if (!String.IsNullOrEmpty(EmployeeLastName))
            {
                сотрудник = сотрудник.Where(t => t.Имя_сотрудника.Contains(EmployeeLastName));
            }
            if (!String.IsNullOrEmpty(EmployeeAddress))
            {
                сотрудник = сотрудник.Where(t => t.Домашний_адрес_сотрудника.Contains(EmployeeAddress));
            }
            if (!String.IsNullOrEmpty(Education))
            {
                сотрудник = сотрудник.Where(t => t.Образование.Contains(Education));
            }
            if (!String.IsNullOrEmpty(Specialization))
            {
                сотрудник = сотрудник.Where(t => t.Специализация.Contains(Specialization));
            }
            return View(сотрудник.ToList());
        }

        // GET: Сотрудник/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            if (сотрудник == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник);
        }

        // POST: Сотрудник/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            db.Сотрудник.Remove(сотрудник);
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
