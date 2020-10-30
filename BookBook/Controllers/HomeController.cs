using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using BookBook.Data;
using PagedList;

namespace BookBook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            eCommerceContext db = new eCommerceContext();
            var list = db.Products.ToList();

            if (page == null)
                page = 1;
            var _list = db.Products.OrderBy(m => m.ProductId);

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.p = list;
            return View(_list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult SearchItem(string searchname)
        {
            eCommerceContext db = new eCommerceContext();
            var product = db.Products.FirstOrDefault(m => m.ProductName == searchname);

            return View(product);
        }

        public ActionResult Sort()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sort(string sort)
        {
            eCommerceContext db = new eCommerceContext();
            var list = db.Products.ToList();

            if (sort == "Giá giảm dần")
            {

                list = db.Database.SqlQuery<Product>(@"select * from Products p order by p.ProductPrice desc").ToList();
                return View(list);
            }
            if (sort == "Giá tăng dần")
            {
                list = db.Database.SqlQuery<Product>(@"select * from Products p order by p.ProductPrice").ToList();
                return View(list);
            }
            if (sort == "Tên A-Z")
            {
                list = db.Database.SqlQuery<Product>(@"select * from Products p order by p.ProductName").ToList();
                return View(list);
            }
            if (sort == "Tên Z-A")
            {
                list = db.Database.SqlQuery<Product>(@"select * from Products p order by p.ProductName desc").ToList();
                return View(list);
            }

            return View(list);
        }
        public ActionResult BieuDO()
        {
            return View();
        }
        public ActionResult GetData()
        {
            eCommerceContext db = new eCommerceContext();

            var ds = db.BillDetails
                 .GroupBy(p => p.Product.ProductName)
                 .Select(s => new { name = s.Key, y = s.Sum(w => w.Quantity) }).ToList();
            return Json(ds, JsonRequestBehavior.AllowGet);
        }
    }
}