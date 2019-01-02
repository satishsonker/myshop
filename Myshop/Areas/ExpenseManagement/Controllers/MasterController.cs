using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.ExpenseManagement.Controllers
{
    public class MasterController : Controller
    {
        // GET: ExpenseManagement/Master
        public ActionResult AddExpenseType()
        {
            return View();
        }
    }
}