using Myshop.Areas.SalesManagement.Models;
using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Myshop.App_Start.Enums;

namespace Myshop.Areas.SalesManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class SaleController : CommonController
    {
        // GET: SalesManagement/Sale
        public ActionResult Dashboard()
        {
            DashboardDetails _details = new DashboardDetails();
            _details.InitializeSalesSetting();
            return View();
        }

        public ActionResult SalesList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CancelInvoice()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CancelInvoice(int invoiceId,string invoiceRemark)
        {
            SalesDetails _details = new SalesDetails();
            return Json(ReturnAjaxAlertMessage(_details.CancelInvoice(invoiceId, invoiceRemark)).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewSales()
        {
            return View();
        }

        public ActionResult GetInvoice()
        {
            return View();
        }

        public ActionResult ReturnInvoice()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetDashboard(int Days)
        {
            DashboardDetails _details = new DashboardDetails();
            return Json(_details.GetDashboardData(Days), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSalesChartData(int Days)
        {
            DashboardDetails _details = new DashboardDetails();
            return Json(_details.GetSalesChartData(Days), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSalesStatusChartData(int Days)
        {
            DashboardDetails _details = new DashboardDetails();
            return Json(_details.GetSalesStatusData(Days), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchProduct(string SearchValue)
        {
            SalesDetails _details = new SalesDetails();
          return Json(_details.SearchProduct(SearchValue),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchInvoice(string SearchValue)
        {
            SalesDetails _details = new SalesDetails();
            return Json(_details.SearchInvoice(SearchValue), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveInvoice(InvoiceDetails invoiceDetails)
        {
            if (ModelState.IsValid)
            {
                SalesDetails _details = new SalesDetails();
            Tuple<CrudStatus, int> _result = _details.SaveInvoice(invoiceDetails);
            return Json(ReturnAjaxAlertMessage(_result.Item1,_result.Item2).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(GetErrorList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveReturnInvoice(InvoiceReturnDetails invoiceReturnDetails)
        {
            if (ModelState.IsValid)
            {
                SalesDetails _details = new SalesDetails();
                return Json(ReturnAjaxAlertMessage(_details.SaveReturnInvoice(invoiceReturnDetails)).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(GetErrorList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetSalesList(int PageNo=1,int PageSize=10)
        {
            SalesDetails _details = new SalesDetails();
            return Json(_details.GetSalesList(PageNo,PageSize), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCustomer(string FirstName, string LastName, string CustMobile,int State,int City)
        {
            if (ModelState.IsValid)
            {
                SalesDetails _details = new SalesDetails();
                return Json(ReturnAjaxAlertMessage(_details.AddCustomer(FirstName, LastName, CustMobile, State, City)).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(GetErrorList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}