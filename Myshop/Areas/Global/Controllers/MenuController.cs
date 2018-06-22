using Myshop.App_Start;
using Myshop.Areas.Global.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Filters;
using Myshop.GlobalResource;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class MenuController : Controller
    {
        // GET: Global/Menu
        public ActionResult AddAppModule()
        {
            return View();
        }
        public ActionResult SetAppModule(AppModuleModel model)
        {
            if(ModelState.IsValid)
            {
                MenuDetails _details = new MenuDetails();
                Enums.CrudStatus status = _details.SetAppModule(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("AddAppModule");
        }
        public ActionResult UpdateAppModule(AppModuleModel model)
        {
           
            if (ModelState.IsValid)
            {
                MenuDetails _details = new MenuDetails();
                Enums.CrudStatus status = _details.SetAppModule(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("AddAppModule");
        }
        public ActionResult DeleteAppModule(AppModuleModel model)
        {
            
            if (ModelState.IsValid)
            {
                MenuDetails _details = new MenuDetails();
                Enums.CrudStatus status = _details.SetAppModule(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("AddAppModule");
        }
        public JsonResult GetAppModules()
        {
            try
            {
                return Json(GlobalMethod.GetAppModules());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetAppModuleJson()
        {
            try
            {
                MenuDetails model = new MenuDetails();
                return Json(model.GetAppModuleJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        public ActionResult AddAppPage()
        {
            return View();
        }
        public ActionResult SetAppPage(AppPageModel model)
        {
            if (ModelState.IsValid)
            {
                MenuDetails _details = new MenuDetails();
                Enums.CrudStatus status = _details.SetAppPage(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("AddAppPage");
        }
        public ActionResult UpdateAppPage(AppPageModel model)
        {

            if (ModelState.IsValid)
            {
                MenuDetails _details = new MenuDetails();
                Enums.CrudStatus status = _details.SetAppPage(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("AddAppPage");
        }
        public ActionResult DeleteAppPage(AppPageModel model)
        {

            if (ModelState.IsValid)
            {
                MenuDetails _details = new MenuDetails();
                Enums.CrudStatus status = _details.SetAppPage(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("AddAppPage");
        }
        public JsonResult GetAppPages(int moduleId=0)
        {
            try
            {
                MenuDetails model = new MenuDetails();
                return Json(model.GetAppPages(moduleId));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetAppPageJson(int moduleid=0)
        {
            try
            {
                MenuDetails model = new MenuDetails();
                return Json(model.GetAppPageJson(moduleid));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        private void SetAlertMessage(string message, Enums.AlertType alert)
        {
            ViewBag.message = message;
            TempData["messages"] = message;
            ViewBag.alert = alert.ToString();
            TempData["alert"] = alert.ToString();
        }

        private void ReturnAlertMessage(Enums.CrudStatus status)
        {
            if (status == Enums.CrudStatus.Deleted)
            {
                SetAlertMessage(Resource.DataDeleted, Enums.AlertType.success);
            }
            else if (status == Enums.CrudStatus.Inserted)
            {
                SetAlertMessage(Resource.DataSaved, Enums.AlertType.success);
            }
            else if (status == Enums.CrudStatus.Updated)
            {
                SetAlertMessage(Resource.DataUpdated, Enums.AlertType.success);
            }
            else if (status == Enums.CrudStatus.AlreadyExistForSameShop)
            {
                SetAlertMessage(Resource.DataExistWithSameShopName, Enums.AlertType.info);
            }
            else if (status == Enums.CrudStatus.NoEffect)
            {
                SetAlertMessage(Resource.DataNotSaved, Enums.AlertType.warning);
            }
            else if (status == Enums.CrudStatus.Exception)
            {
                SetAlertMessage(Resource.Exception, Enums.AlertType.danger);
            }
            else if (status == Enums.CrudStatus.AlreadyInUse)
            {
                SetAlertMessage(Resource.AlreadyInUse, Enums.AlertType.warning);
            }
        }

    }
}