using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using BookBook.Data;
using BookBook.Database;

namespace BookBook.Controllers
{
    public class discountsController : Controller
    {
        //private eCommerceContext db = new eCommerceContext();
        private BookEntity context = new BookEntity();

        // GET: discounts
        public ActionResult Index()
        {
            string query = @"select * from discounts where status > 0";
            var list = context.Database.SqlQuery<discount>(query).ToList();
            return View(list);
        }

        // GET: discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discount discount = context.discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: discounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(discount _discount)
        {
            if (ModelState.IsValid)
            {
                _discount.alterdate = DateTime.Now;
                _discount.createdate = DateTime.Now;
                _discount.createuser = "Admin";
                _discount.alteruser = "Admin";
                _discount.status = 1;

                context.discounts.Add(_discount);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_discount);
        }

        // GET: discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discount _discount = context.discounts.Find(id);
            if (_discount == null)
            {
                return HttpNotFound();
            }
            return View(_discount);
        }

        // POST: discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(discount _discount)
        {
            using (var dbTran = context.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        var temp = context.discounts.Find(_discount.id);

                        temp.name = (_discount.name != null || _discount.name != "") ? _discount.name : temp.name;
                        temp.discount_percent = (_discount.discount_percent != 0) ? _discount.discount_percent : temp.discount_percent;
                        temp.quantity = (_discount.quantity != 0) ? _discount.quantity : temp.quantity;
                        temp.datevalid = (_discount.datevalid != null) ? _discount.datevalid : temp.datevalid;

                        context.Entry(temp).State = EntityState.Modified;
                        context.SaveChanges();

                        dbTran.Commit();
                        return RedirectToAction("Index");
                    }
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }

                    dbTran.Rollback();

                    return View(_discount);
                }
            }

            return View(_discount);
        }

        // GET: discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discount _discount = context.discounts.Find(id);

            if (_discount == null)
            {
                return HttpNotFound();
            }

            _discount.status = 0;
            _discount.alterdate = DateTime.Now;
            context.Entry(_discount).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
