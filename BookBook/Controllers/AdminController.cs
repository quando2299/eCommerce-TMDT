using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookBook.Database;
using BookBook.Models;

namespace BookBook.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            BookEntity context = new BookEntity();
            var list = context.Database.SqlQuery<user>(@"select * from users where isadmin <> 1").ToList();
            return View(list);
        }

        public ActionResult Product()
        {
            BookEntity context = new BookEntity();
            var list = context.Database.SqlQuery<ProductsModel>(
                @"select p.name name, p.price price, p.author author, p.publishing_company publishing_company, p.description description, p.image image, t.name typename, p.quantity quantity
                from products p
	                inner join type t on t.id = p.typeid").ToList();
            return View(list);
        }

        public ActionResult Orders()
        {
            BookEntity context = new BookEntity();
            var list = context.Database.SqlQuery<OrdersModel>(
                @"select o.id, o.userid, o.total, o.createdate, o.status, concat(u.firstname, ' ', u.lastname) as fullname 
                from orders o
	                inner join users u on u.id = o.userid").ToList();
            return View(list);
        }
    }
}