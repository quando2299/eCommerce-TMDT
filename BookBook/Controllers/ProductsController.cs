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
//using PagedList;
//using PagedList.Mvc;

namespace BookBook.Controllers
{
    public class ProductsController : Controller
    {
        eCommerceContext db = new eCommerceContext();

        // GET: Products
        public ActionResult Index()
        {

            //var list = db.Products.ToList();
            ViewData["Danhsach"] = db.Products.ToList();
            return View();
        }



        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Types, "TypeID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.Types, "TypeID", "Name", product.TypeId);
            return View(product);
        }


        public ActionResult Edit(int? id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var temp = db.Products.FirstOrDefault(m => m.ProductId == product.ProductId);

            temp.ProductName = product.ProductName;
            temp.ProducQuantity = product.ProducQuantity;
            temp.Tacgia = product.Tacgia;
            temp.ProductPrice = product.ProductPrice;
            temp.Description = product.Description;

            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }
         
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            db.Products.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var product = db.Products.FirstOrDefault(m => m.ProductId == id);

            return View(product);
        }

        public ActionResult DetailProduct(int? id)
        {
            Product product = db.Products.FirstOrDefault(m => m.ProductId == id);

            return View(product);
        }

        public ActionResult ProductByType(int? id)
        {
            var list = db.Database.SqlQuery<ProductByType>(@"
                        select p.ProductId, p.ProductName, p.ProductPrice, p.ProductImage
                        from Products p
                            inner join Types t on t.TypeID = p.TypeId
                        where t.TypeID = {0}
                        ", id).ToList();
            return View(list);
        }
    }
}
