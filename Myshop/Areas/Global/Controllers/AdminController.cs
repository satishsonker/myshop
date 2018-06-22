using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Areas.Global.Models;
using Myshop.Filters;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class AdminController : Controller
    {
        // GET: Global/Admin
        public ActionResult GetErrorLog(bool isAllLog=false)
        {
            AdminDetails details = new AdminDetails();
            return View(details.GetErrorLog(isAllLog));
        }

        public JsonResult UpdateErrorLog(int ErrorId)
        {
            AdminDetails details = new AdminDetails();
            return Json(details.UpdateErrorLog(ErrorId));
        }
    }
}