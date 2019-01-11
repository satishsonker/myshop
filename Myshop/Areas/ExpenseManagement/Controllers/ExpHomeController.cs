using Myshop.Areas.ExpenseManagement.Models;
using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.ExpenseManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class ExpHomeController : CommonController
    {
        // GET: ExpenseManagement/ExpHome
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetMonthlyExpenseChart(int Year,int Month)
        {
            ExpHomeDetails expHomeDetails = new ExpHomeDetails();
            return Json(expHomeDetails.MonthlyExpChart(Year, Month), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TopExpenses(int Year, int Month)
        {
            ExpHomeDetails expHomeDetails = new ExpHomeDetails();
            return Json(expHomeDetails.TopExpenses(Year, Month), JsonRequestBehavior.AllowGet);
        }
    }
}