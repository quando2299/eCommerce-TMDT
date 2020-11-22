using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
//using BookBook.Data;
using BookBook.Database;
using PagedList;

namespace BookBook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();

            //var list = db.Products.ToList();
            var list = context.products.ToList();

            if (page == null)
                page = 1;
            var _list = context.products.OrderBy(m => m.id);

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            ViewBag.p = list;

            return View(_list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult SearchItem(string searchname)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();
            var product = context.products.FirstOrDefault(m => m.name == searchname);

            return View(product);
        }

        public ActionResult Sort()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sort(string sort)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();
            var list = context.products.ToList();

            if (sort == "Giá giảm dần")
            {

                list = context.Database.SqlQuery<product>(@"select * from products p order by p.price desc").ToList();
                return View(list);
            }
            if (sort == "Giá tăng dần")
            {
                list = context.Database.SqlQuery<product>(@"select * from products p order by p.price").ToList();
                return View(list);
            }
            if (sort == "Tên A-Z")
            {
                list = context.Database.SqlQuery<product>(@"select * from products p order by p.name").ToList();
                return View(list);
            }
            if (sort == "Tên Z-A")
            {
                list = context.Database.SqlQuery<product>(@"select * from products p order by p.name desc").ToList();
                return View(list);
            }

            return View(list);
        }
    }
}