using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using BookBook.Data;
using BookBook.Database;

namespace BookBook.Controllers
{
    public class TypesController : BaseController
    {
        //private eCommerceContext db = new eCommerceContext();
        private BookEntity context = new BookEntity();

        public ActionResult Index()
        {
            //return View(db.Types.ToList());
            return View(context.types.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] type _type)
        {
            if (ModelState.IsValid)
            {
                context.types.Add(_type);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_type);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            type _type = context.types.Find(id);
            if (_type == null)
            {
                return HttpNotFound();
            }
            return View(_type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")]type _type)
        {
            if (ModelState.IsValid)
            {
                context.Entry(_type).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_type);
        }
        public ActionResult Delete(int id)
        {
            type _type = context.types.Find(id);
            if (_type == null)
                return RedirectToAction("Index");

            _type.status = 0;

            context.Entry(_type).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
