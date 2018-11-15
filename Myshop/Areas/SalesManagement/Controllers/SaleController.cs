using Myshop.Areas.SalesManagement.Models;
using Myshop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Myshop.App_Start.Enums;

namespace Myshop.Areas.SalesManagement.Controllers
{
    public class SaleController : CommonController
    {
        // GET: SalesManagement/Sale
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult NewSales()
        {
            return View();
        }

        public JsonResult SearchProduct(string SearchValue)
        {
            SalesDetails _details = new SalesDetails();
          return Json(_details.SearchProduct(SearchValue),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveInvoice(InvoiceDetails invoiceDetails)
        {
            SalesDetails _details = new SalesDetails();
            Tuple<CrudStatus, int> _result = _details.SaveInvoice(invoiceDetails);
            return Json(ReturnAjaxAlertMessage(_result.Item1,_result.Item2).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddCustomer(string FirstName, string LastName, string CustMobile)
        {
            SalesDetails _details = new SalesDetails();
            return Json(ReturnAjaxAlertMessage(_details.AddCustomer(FirstName,LastName,CustMobile)).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}