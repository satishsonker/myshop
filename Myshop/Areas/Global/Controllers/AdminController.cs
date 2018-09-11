using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Areas.Global.Models;
using Myshop.Filters;
using Myshop.Controllers;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class AdminController : CommonController
    {
        // GET: Global/Admin
        public ActionResult GetErrorLog(bool isAllLog=false)
        {
            AdminDetails details = new AdminDetails();
            return View(details.GetErrorLog(isAllLog));
        }

        [HttpGet]
        public ActionResult ResetUserPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ResetUserPassword(int _userId)
        {
            AdminDetails details = new AdminDetails();
            return Json(ReturnAjaxAlertMessage(details.ResetUserPassword(_userId)).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateErrorLog(int ErrorId)
        {
            AdminDetails details = new AdminDetails();
            return Json(details.UpdateErrorLog(ErrorId));
        }
    }
}