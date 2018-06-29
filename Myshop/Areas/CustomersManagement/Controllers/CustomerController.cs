using Myshop.Areas.CustomersManagement.Models;
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
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCustomesChartData(int[] CustTypeId, int Duration = 30)
        {
            DashboardDetail _details = new DashboardDetail();
            return Json(_details.GetCustomesChartData(CustTypeId, Duration), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTotalCustomerTypePieChartData(int[] CustTypeId, int Duration = 30)
        {
            DashboardDetail _details = new DashboardDetail();
            return Json(_details.GetTotalCustomerTypeChartData(CustTypeId, Duration), JsonRequestBehavior.AllowGet);
        }
    }
}