//using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using BookBook.Database;
using BookBook.Models.Utils;

namespace BookBook.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageBill()
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();

            var list = context.Database.SqlQuery<BillView>(@"
                    select concat(u.FirstName, ' ', u.LastName) as FullName, b.Total, b.CreateDate, b.CreateUser, b.Status, b.ID
                    from Users u
                        inner join Bill b on b.UserID = u.UserId
                ", (int)Session["Account"]).ToList();
            
            return View(list);
        }

        public ActionResult Buy(int id)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();

            var product = context.products.FirstOrDefault(m => m.id == id);
            var itemCart = new Cart();
            if (Session["Cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                itemCart.id = product.id;///
                itemCart.name = product.name;
                itemCart.price = product.price;
                itemCart.quantity = 1;
                itemCart.image = product.image;

                cart.Add(itemCart);
                Session["Cart"] = cart;
            }
            else
            {
                var list = Session["Cart"] as List<Cart>;
                int index = isExist(id);

                if (index != -1)
                {
                    list[index].quantity += 1;
                }
                else
                {
                    List<Cart> cart = Session["Cart"] as List<Cart>;
                    itemCart.id = product.id;///
                    itemCart.name = product.name;
                    itemCart.price = product.price;
                    itemCart.quantity = 1;
                    itemCart.image = product.image;

                    cart.Add(itemCart);
                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            var list = Session["Cart"] as List<Cart>;
            int index = list.FindIndex(m => m.id == id);

            return index;
        }

        public ActionResult Remove(int id)
        {
            var list = Session["Cart"] as List<Cart>;
            list.RemoveAt(isExist(id));
            Session["Cart"] = list;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CheckOut(user user, string code)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();

            var list = Session["Cart"] as List<Cart>;

            var discount = context.discounts.FirstOrDefault(m => m.name == code);
            int discountID = discount.id;
            var userID = (int)Session["Account"];
            var _user = context.users.FirstOrDefault(m => m.id == userID);

            int result = list.Sum(item => item.price) * (100 - (int)CheckDiscount(code)) / 100;
            
            CheckOutView view = new CheckOutView();
            view.FullName = _user.firstname + " " + _user.lastname;
            view.Total = result;
            view.DiscountID = discountID;
            view.UserID = userID;

            return View(view);
        }

        [HttpPost]
        //public ActionResult ConfirmCheckOut(CheckOutView view)
        public ActionResult ConfirmCheckOut(CheckOutView view)
        {
            //eCommerceContext db = new eCommerceContext();
            //var user = db.Users.FirstOrDefault(m => m.UserId == view.UserID);
            //Bill bill = new Bill();
            //bill.UserID = view.UserID;
            //bill.Total = view.Total;
            //bill.DiscountID = view.DiscountID;
            //bill.Status = 1;
            //bill.CreateDate = DateTime.Now;
            //bill.CreateUser = user.FirstName + " " + user.LastName;
            //bill.UpdateDate = DateTime.Now;
            //bill.UpdateUser = user.FirstName + " " + user.LastName;

            //db.Bills.Add(bill);
            //db.SaveChanges();

            //BillDetail billDetail = new BillDetail();

            //foreach (var item in Session["Cart"] as List<Cart>)
            //{
            //    billDetail.BillID = bill.ID;
            //    billDetail.ProductID = item.ProductId;
            //    billDetail.Quantity = item.Quantity;
            //    billDetail.Status = 1;
            //    billDetail.CreateDate = DateTime.Now;
            //    billDetail.CreateUser = user.FirstName + " " + user.LastName;
            //    billDetail.UpdateDate = DateTime.Now;
            //    billDetail.UpdateUser = user.FirstName + " " + user.LastName;

            //    db.BillDetails.Add(billDetail);
            //    db.SaveChanges();
            //}

            //Session.Remove("Cart");
            BookEntity context = new BookEntity();
            var user = context.users.Find((int)Session["Account"]);
            var userName = user.firstname + " " + user.lastname;

            string content = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Content/Template/ConfirmBillForm.html"));
            content = content.Replace("{{username}}", userName);

            MailSender.SendEmail(user.email, userName, "Xác nhận đơn hàng !", content, null);

            return RedirectToAction("Index", "Home");
        }

        public double CheckDiscount(string Code)
        {
            //eCommerceContext db = new eCommerceContext();
            BookEntity context = new BookEntity();

            var discount = context.discounts.Where(x => x.name.ToString() == Code).SingleOrDefault();
            if (discount != null &&
                discount.quantity > 0 &&
                discount.createdate <= DateTime.Now)
            {
                return discount.discount_percent;
            }
            return -1;
        }
    }
}