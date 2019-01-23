using Myshop.Areas.ExpenseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.ExpenseManagement.Controllers
{
    public class ReportsController : Controller
    {
        // GET: ExpenseManagement/Reports
        [HttpGet]
        public ActionResult BalanceReport()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetBalanceReport(int Year=0,int Month=0,int VendorId=0)
        {
            ReportsDetails reportsDetails = new ReportsDetails();
            return Json(reportsDetails.GetBalanceReport(Year,Month,VendorId),JsonRequestBehavior.AllowGet);
        }
    }
}