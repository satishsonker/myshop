using Myshop.Areas.SalesManagement.Models;
using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Myshop.Areas.SalesManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class ReportsController : CommonController
    {
        ReportsDetails reportsDetails = null;
        // GET: SalesManagement/Reports
        [HttpGet]
        public ActionResult GetStatements()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetGstStatements()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MostSellingProduct()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSalesStatement(DateTime FromDate,DateTime ToDate)
        {
            reportsDetails = new ReportsDetails();
            return Json(reportsDetails.GetStatement(FromDate, ToDate).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMostSaleProduct(DateTime FromDate, DateTime ToDate, int PageNo=1, int PageSize=10)
        {
            reportsDetails = new ReportsDetails();
            return Json(reportsDetails.ProductsMostSalling(FromDate, ToDate, PageNo, PageSize).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGstStatement(DateTime FromDate, DateTime ToDate)
        {
            reportsDetails = new ReportsDetails();
            return Json(reportsDetails.GetGstStatement(FromDate, ToDate).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}