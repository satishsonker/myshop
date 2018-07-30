using Myshop.Filters;
using Myshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Controllers
{
    public class UsersController : CommonController
    {
        UsersDetails _detail = null;
        // GET: Users
        [MyshopAuthorize]
        [MyShopPermission]
        public ActionResult NotificationList()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUserNotificationList()
        {
            _detail = new UsersDetails();
            return Json(_detail.GetUserNotificationList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUserNotificationList(int notificationId)
        {
            _detail = new UsersDetails();
            return Json(ReturnAjaxAlertMessage(_detail.DeleteUserNotificationList(notificationId)).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}