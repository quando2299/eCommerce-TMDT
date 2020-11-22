using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookBook.Data;
using BookBook.Models;
using BookBook.Database;
//using PagedList;
//using PagedList.Mvc;

namespace BookBook.Controllers
{
    public class ProductsController : Controller
    {
        //eCommerceContext db = new eCommerceContext();
        BookEntity context = new BookEntity();

        // GET: Products
        public ActionResult Index()
        {

            //var list = context.products.ToList();
            ViewData["Danhsach"] = context.products.ToList();
            return View();
        }



        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(context.types, "TypeID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(product product)
        {
            if (ModelState.IsValid)
            {
                context.products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(context.types, "TypeID", "Name", product.typeid);
            return View(product);
        }


        public ActionResult Edit(int? id)
        {
            var product = context.products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(product product)
        {
            var temp = context.products.FirstOrDefault(m => m.id == product.id);

            temp.name = product.name;
            temp.quantity = product.quantity;
            temp.author = product.author;
            temp.price= product.price;
            temp.description = product.description;

            context.Entry(temp).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
         
        public ActionResult Delete(int id)
        {
            var item = context.products.Find(id);
            context.products.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var product = context.products.FirstOrDefault(m => m.id == id);

            return View(product);
        }

        public ActionResult DetailProduct(int? id)
        {
            product product = context.products.FirstOrDefault(m => m.id == id);

            return View(product);
        }

        public ActionResult ProductByType(int? id)
        {
            var list = context.Database.SqlQuery<ProductByType>(@"
                        select p.id, p.name, p.price, p.image
                        from products p
                            inner join types t on t.id = p.typeid
                        where t.id = {0}
                        ", id).ToList();
            return View(list);
        }
    }
}
