using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.CustomersManagement.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomersManagement/Customer
        public ActionResult Home()
        {
            return View();
        }
    }
}