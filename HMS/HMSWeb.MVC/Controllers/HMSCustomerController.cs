using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectClassLibrary;
using HMS.Services;

namespace HMSWeb.MVC.Controllers
{
    public class HMSCustomerController : Controller
    {
        private readonly ICustomer cust;

        public HMSCustomerController(ICustomer cust)
        {
            this.cust = cust;
        }

        // GET: HMSCustomer
        public ActionResult Index()
        {
            return View();
        }

        // Post: HMSCustomer
        [HttpPost]
        public ActionResult Index(Customer model)
        {
            var c = cust.Validate(model);

            if (c == null)
            {
                ViewBag.Message = "Invalid UserName and Password";
            }
            else
            {
                Session["name"] = c.Firstname;
                Session["id"] = c.CustomerId;
                return RedirectToAction("CustomerHome");
            }


            return View(model);
        }

        public ActionResult CustomerHome()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "HMSCustomer");
        }





    }
}