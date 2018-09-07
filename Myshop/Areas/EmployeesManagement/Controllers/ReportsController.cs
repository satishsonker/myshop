using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.EmployeesManagement.Controllers
{
    [MyshopAuthorize]
    public class ReportsController : CommonController
    {
        // GET: EmployeesManagement/Reports
        public ActionResult Index()
        {
            return View();
        }
    }
}