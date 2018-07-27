using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Models;
using Myshop.Filters;

namespace Myshop.Controllers
{
    public class HomeController : CommonController
    {       
        [MyshopAuthorize]
        [MyShopPermission]
        public ActionResult Dashboard()
        {
            return View();
        }

        [MyshopAuthorize]
        [MyShopPermission]
        public ActionResult Index()
        {
            return View();
        }

        [MyshopAuthorize]
        [MyShopPermission]
        public ActionResult NotificationMessage()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}