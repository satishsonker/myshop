using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.SalesManagement.Controllers
{
    public class SaleController : Controller
    {
        // GET: SalesManagement/Sale
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}