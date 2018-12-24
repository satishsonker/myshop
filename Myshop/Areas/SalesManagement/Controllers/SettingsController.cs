using Myshop.Areas.SalesManagement.Models;
using Myshop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Myshop.App_Start.Enums;
using Myshop.Filters;

namespace Myshop.Areas.SalesManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    [MyshopDowntime]
    public class SettingsController : CommonController
    {
        // GET: SalesManagement/Settings
        [HttpGet]
        public ActionResult GetSetting()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveSetting(SalesSettingModel model)
        {
            if (ModelState.IsValid)
            {
                SalesSettingDetails salesSettingDetails = new SalesSettingDetails();
                ReturnAlertMessagToView(salesSettingDetails.SaveSetting(model, CrudType.Insert));
            }
           return View("GetSetting");
        }

        [HttpPost]
        public ActionResult UpdateSetting(SalesSettingModel model)
        {
            SalesSettingDetails salesSettingDetails = new SalesSettingDetails();
            ReturnAlertMessagToView(salesSettingDetails.SaveSetting(model, CrudType.Update));
            return View("GetSetting");
        }

        [HttpPost]
        public ActionResult DeleteSetting(SalesSettingModel model)
        {
            SalesSettingDetails salesSettingDetails = new SalesSettingDetails();
            ReturnAlertMessagToView(salesSettingDetails.SaveSetting(model, CrudType.Delete));
            return View("GetSetting");
        }

        [HttpPost]
        public JsonResult GetSaleSettingJson()
        {
            SalesSettingDetails salesSettingDetails = new SalesSettingDetails();
            return Json(salesSettingDetails.GetSalesSetting(), JsonRequestBehavior.AllowGet);
        }
    }
}