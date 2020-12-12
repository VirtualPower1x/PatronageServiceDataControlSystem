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
    public class DiscountNotLessOrEqualException : DataException
    {
        public DiscountNotLessOrEqualException (string message) : base (message)
        {

        }
    }

    public class NegativeServicePeriodException : DataException
    {
        public NegativeServicePeriodException(string message) : base(message)
        {

        }
    }

    public class Талон_ухода_за_клиентомController : Controller
    {        
        private PatronageServiceModel db = new PatronageServiceModel();

        // GET: Талон_ухода_за_клиентом
        public ActionResult Index()
        {
            var талон_ухода_за_клиентом = db.Талон_ухода_за_клиентом.Include(т => т.Клиент).Include(т => т.Сотрудник).Include(т => т.Услуга);
            return View(талон_ухода_за_клиентом.ToList());
        }

        // GET: Талон_ухода_за_клиентом/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Талон_ухода_за_клиентом талон_ухода_за_клиентом = db.Талон_ухода_за_клиентом.Find(id);
            if (талон_ухода_за_клиентом == null)
            {
                return HttpNotFound();
            }
            return View(талон_ухода_за_клиентом);
        }

        // GET: Талон_ухода_за_клиентом/Create
        public ActionResult Create()
        {
            ViewBag.ID_клиента = new SelectList(db.Клиент, "ID_клиента", "Фамилия_клиента");
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник, "ID_сотрудника", "Фамилия_сотрудника");
            ViewBag.ID_услуги = new SelectList(db.Услуга, "ID_услуги", "Название_услуги");
            return View();
        }

        // POST: Талон_ухода_за_клиентом/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_талона,ID_сотрудника,ID_клиента,ID_услуги,Дата_начала_обслуживания,Дата_окончания_обслуживания,Схема_мероприятий,Медицинские_показания,Сумма_к_оплате,Скидка")] Талон_ухода_за_клиентом талон_ухода_за_клиентом)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    СотрудникController employeeCtrlr = new СотрудникController();
                    УслугаController serviceCtrlr = new УслугаController();
                    double MaxDiscountValue = serviceCtrlr.MaxDiscount(талон_ухода_за_клиентом.ID_услуги);

                    if (талон_ухода_за_клиентом.Скидка > MaxDiscountValue)
                    {
                        DiscountNotLessOrEqualException ex = new DiscountNotLessOrEqualException("Скидка не может быть больше максимальной, установленной для данной услуги.");
                        throw ex;
                    }
                    else if (талон_ухода_за_клиентом.Дата_начала_обслуживания > талон_ухода_за_клиентом.Дата_окончания_обслуживания)
                    {
                        NegativeServicePeriodException ex = new NegativeServicePeriodException("Дата начала обслуживания не может быть позднее даты окончания обслуживания.");
                        throw ex;
                    }
                    else
                    {
                        db.Талон_ухода_за_клиентом.Add(талон_ухода_за_клиентом);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (DiscountNotLessOrEqualException ex)
                {
                    ViewData["DiscountErrorMessage"] = ex.Message;
                }
                catch (NegativeServicePeriodException ex)
                {
                    ViewData["DatesErrorMessage"] = ex.Message;
                }                
            }

            ViewBag.ID_клиента = new SelectList(db.Клиент, "ID_клиента", "Фамилия_клиента", талон_ухода_за_клиентом.ID_клиента);
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник, "ID_сотрудника", "Фамилия_сотрудника", талон_ухода_за_клиентом.ID_сотрудника);
            ViewBag.ID_услуги = new SelectList(db.Услуга, "ID_услуги", "Название_услуги", талон_ухода_за_клиентом.ID_услуги);
            return View(талон_ухода_за_клиентом);
        }

        // GET: Талон_ухода_за_клиентом/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Талон_ухода_за_клиентом талон_ухода_за_клиентом = db.Талон_ухода_за_клиентом.Find(id);
            if (талон_ухода_за_клиентом == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_клиента = new SelectList(db.Клиент, "ID_клиента", "Фамилия_клиента", талон_ухода_за_клиентом.ID_клиента);
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник, "ID_сотрудника", "Фамилия_сотрудника", талон_ухода_за_клиентом.ID_сотрудника);
            ViewBag.ID_услуги = new SelectList(db.Услуга, "ID_услуги", "Название_услуги", талон_ухода_за_клиентом.ID_услуги);
            return View(талон_ухода_за_клиентом);
        }

        // POST: Талон_ухода_за_клиентом/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_талона,ID_сотрудника,ID_клиента,ID_услуги,Дата_начала_обслуживания,Дата_окончания_обслуживания,Схема_мероприятий,Медицинские_показания,Сумма_к_оплате,Скидка")] Талон_ухода_за_клиентом талон_ухода_за_клиентом)
        {            
            if (ModelState.IsValid)
            {
                try
                {
                    УслугаController serviceCtrlr = new УслугаController();
                    double MaxDiscountValue = serviceCtrlr.MaxDiscount(талон_ухода_за_клиентом.ID_услуги);
                    if (талон_ухода_за_клиентом.Скидка > MaxDiscountValue)
                    {
                        DiscountNotLessOrEqualException ex = new DiscountNotLessOrEqualException("Скидка не может быть больше максимальной, установленной для данной услуги.");
                        throw ex;
                    }
                    else if (талон_ухода_за_клиентом.Дата_начала_обслуживания > талон_ухода_за_клиентом.Дата_окончания_обслуживания)
                    {
                        NegativeServicePeriodException ex = new NegativeServicePeriodException("Дата начала обслуживания не может быть позднее даты окончания обслуживания.");
                        throw ex;
                    }
                    else
                    {
                        db.Entry(талон_ухода_за_клиентом).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (DiscountNotLessOrEqualException ex)
                {
                    ViewData["DiscountErrorMessage"] = ex.Message;
                }
                catch (NegativeServicePeriodException ex)
                {
                    ViewData["DatesErrorMessage"] = ex.Message;
                }
            }
            ViewBag.ID_клиента = new SelectList(db.Клиент, "ID_клиента", "Фамилия_клиента", талон_ухода_за_клиентом.ID_клиента);
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник, "ID_сотрудника", "Фамилия_сотрудника", талон_ухода_за_клиентом.ID_сотрудника);
            ViewBag.ID_услуги = new SelectList(db.Услуга, "ID_услуги", "Название_услуги", талон_ухода_за_клиентом.ID_услуги);
            return View(талон_ухода_за_клиентом);
        }

        public ActionResult Filter(int? ServicesID, string ServiceDateMin, string ServiceDateMax)
        {
            ViewBag.ServicesID = new SelectList(db.Услуга, "ID_услуги", "Название_услуги");
            DateTime ServiceDateTimeMin, ServiceDateTimeMax;
            var талон_ухода_за_клиентом = db.Талон_ухода_за_клиентом.Include(т => т.Клиент).Include(т => т.Сотрудник).Include(т => т.Услуга);
            if (ServicesID != null)
            {
                талон_ухода_за_клиентом = талон_ухода_за_клиентом.Where(т => т.ID_услуги == ServicesID);
            }            
            if (!String.IsNullOrEmpty(ServiceDateMin) && DateTime.TryParse(ServiceDateMin, out ServiceDateTimeMin))
            {
                талон_ухода_за_клиентом = талон_ухода_за_клиентом.Where(т => т.Дата_начала_обслуживания >= ServiceDateTimeMin);
            }
            if (!String.IsNullOrEmpty(ServiceDateMax) && DateTime.TryParse(ServiceDateMax, out ServiceDateTimeMax))
            {
                талон_ухода_за_клиентом = талон_ухода_за_клиентом.Where(т => т.Дата_окончания_обслуживания <= ServiceDateTimeMax);
            }
            
            return View(талон_ухода_за_клиентом.ToList());
        }

        public ActionResult Search(string ClientName, string EmployeeName, string ActivitiesPlan, string MedicalDiagnosis)
        {
            var талон_ухода_за_клиентом = db.Талон_ухода_за_клиентом.Include(т => т.Клиент).Include(т => т.Сотрудник).Include(т => т.Услуга);
            if (!String.IsNullOrEmpty(ClientName))
            {
                талон_ухода_за_клиентом = талон_ухода_за_клиентом.Where(т => т.Клиент.Фамилия_клиента.Contains(ClientName));
            }
            if (!String.IsNullOrEmpty(EmployeeName))
            {
                талон_ухода_за_клиентом = талон_ухода_за_клиентом.Where(т => т.Сотрудник.Фамилия_сотрудника.Contains(EmployeeName));
            }
            if (!String.IsNullOrEmpty(ActivitiesPlan))
            {
                талон_ухода_за_клиентом = талон_ухода_за_клиентом.Where(т => т.Схема_мероприятий.Contains(ActivitiesPlan));
            }
            if (!String.IsNullOrEmpty(MedicalDiagnosis))
            {
                талон_ухода_за_клиентом = талон_ухода_за_клиентом.Where(т => т.Медицинские_показания.Contains(MedicalDiagnosis));
            }
            return View(талон_ухода_за_клиентом.ToList());
        }


        // GET: Талон_ухода_за_клиентом/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Талон_ухода_за_клиентом талон_ухода_за_клиентом = db.Талон_ухода_за_клиентом.Find(id);
            if (талон_ухода_за_клиентом == null)
            {
                return HttpNotFound();
            }
            return View(талон_ухода_за_клиентом);
        }

        
        // POST: Талон_ухода_за_клиентом/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Талон_ухода_за_клиентом талон_ухода_за_клиентом = db.Талон_ухода_за_клиентом.Find(id);
            db.Талон_ухода_за_клиентом.Remove(талон_ухода_за_клиентом);
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
