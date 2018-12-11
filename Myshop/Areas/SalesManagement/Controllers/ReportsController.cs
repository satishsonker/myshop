using Myshop.Areas.SalesManagement.Models;
using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.SalesManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class ReportsController : CommonController
    {
        // GET: SalesManagement/Reports
        [HttpGet]
        public ActionResult GetStatements()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSalesStatement(DateTime FromDate,DateTime ToDate)
        {
            ReportsDetails reportsDetails = new ReportsDetails();
            return Json(reportsDetails.GetStatement(FromDate, ToDate).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}