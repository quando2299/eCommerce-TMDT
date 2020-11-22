using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookBook.Database;

namespace BookBook.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["Account"] == null || (int)(Session["Account"]) != 1)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Accounts",
                    action = "Login"
                }));
            }

            base.OnActionExecuted(filterContext);
        }
    }
}