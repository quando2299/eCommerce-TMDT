using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookBook.Database;
using BookBook.Models;

namespace BookBook.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            BookEntity context = new BookEntity();
            return View(context.orders.ToList().Where(model => model.userid == (int)Session["Account"]));
        }

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            BookEntity context = new BookEntity();

            var query =
                @"
                select o.id id, p.name name, od.quantity quantity, (p.price * od.quantity) total
                from orders o
	                inner join order_detail od on od.orderid = o.id
	                inner join products p on p.id = od.productid
                where o.id = " + id;

            var list = context.Database.SqlQuery<OrderDetailModel>(query).ToList();

            return View(list);
        }
    }
}