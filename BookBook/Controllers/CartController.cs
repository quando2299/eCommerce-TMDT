using BookBook.Data;
using BookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            eCommerceContext db = new eCommerceContext();
            var list = db.Database.SqlQuery<BillView>(@"
                    select concat(u.FirstName, ' ', u.LastName) as FullName, b.Total, b.CreateDate, b.CreateUser, b.Status, b.ID
                    from Users u
                        inner join Bill b on b.UserID = u.UserId
                ", (int)Session["Account"]).ToList();
            
            return View(list);
        }

        public ActionResult Buy(int id)
        {
            eCommerceContext db = new eCommerceContext();
            var product = db.Products.FirstOrDefault(m => m.ProductId == id);
            var itemCart = new Cart();
            if (Session["Cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                itemCart.ProductId = product.ProductId;///
                itemCart.ProductName = product.ProductName;
                itemCart.ProductPrice = (int)product.ProductPrice;
                itemCart.Quantity = 1;
                itemCart.ProductImage = product.ProductImage;

                cart.Add(itemCart);
                Session["Cart"] = cart;
            }
            else
            {
                var list = Session["Cart"] as List<Cart>;
                int index = isExist(id);

                if (index != -1)
                {
                    list[index].Quantity += 1;
                }
                else
                {
                    List<Cart> cart = Session["Cart"] as List<Cart>;
                    itemCart.ProductId = product.ProductId;
                    itemCart.ProductName = product.ProductName;
                    itemCart.ProductPrice = (int)product.ProductPrice;
                    itemCart.Quantity = 1;
                    itemCart.ProductImage = product.ProductImage;

                    cart.Add(itemCart);
                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            var list = Session["Cart"] as List<Cart>;
            int index = list.FindIndex(m => m.ProductId == id);

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
        public ActionResult CheckOut(Address address, string code)
        {
            eCommerceContext db = new eCommerceContext();

            var list = Session["Cart"] as List<Cart>;

            var discount = db.Discounts.FirstOrDefault(m => m.Name == code);
            int discountID = discount.DiscountId;
            var userID = (int)Session["Account"];
            var user = db.Users.FirstOrDefault(m => m.UserId == userID);

            int result = list.Sum(item => item.ProductPrice) * (100 - (int)CheckDiscount(code)) / 100;
            CheckOutView view = new CheckOutView();
            view.FullName = user.FirstName + " " + user.LastName;
            view.Total = result;
            view.DiscountID = discountID;
            view.UserID = userID;
            
            return View(view);
        }

        [HttpPost]
        public ActionResult ConfirmCheckOut(CheckOutView view)
        {
            eCommerceContext db = new eCommerceContext();
            var user = db.Users.FirstOrDefault(m => m.UserId == view.UserID);
            Bill bill = new Bill();
            bill.UserID = view.UserID;
            bill.Total = view.Total;
            bill.DiscountID = view.DiscountID;
            bill.Status = 1;
            bill.CreateDate = DateTime.Now;
            bill.CreateUser = user.FirstName + " " + user.LastName;
            bill.UpdateDate = DateTime.Now;
            bill.UpdateUser = user.FirstName + " " + user.LastName;

            db.Bills.Add(bill);
            db.SaveChanges();

            BillDetail billDetail = new BillDetail();

            foreach (var item in Session["Cart"] as List<Cart>)
            {
                billDetail.BillID = bill.ID;
                billDetail.ProductID = item.ProductId;
                billDetail.Quantity = item.Quantity;
                billDetail.Status = 1;
                billDetail.CreateDate = DateTime.Now;
                billDetail.CreateUser = user.FirstName + " " + user.LastName;
                billDetail.UpdateDate = DateTime.Now;
                billDetail.UpdateUser = user.FirstName + " " + user.LastName;

                db.BillDetails.Add(billDetail);
                db.SaveChanges();
            }

            Session.Remove("Cart");
            return RedirectToAction("Index", "Home");
        }   

        public double CheckDiscount(string Code)
        {
            eCommerceContext db = new eCommerceContext();
            var discount = db.Discounts.FirstOrDefault(x => x.Name == Code);
            if (discount != null &&
                discount.QuantityDiscount > 0 &&    
                discount.CreatDate <= DateTime.Now )
            {
                return discount.Percent;
            }
            return -1;
        }
    }
}