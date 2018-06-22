using Myshop.App_Start;
using Myshop.Filters;
using System.Web.Mvc;
using DataLayer;
using Myshop.Areas.Global.Models;
using Myshop.GlobalResource;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class SettingController : Controller
    {
        // GET: Global/Setting
        public ActionResult GetShop()
        {
            ViewBag.ShopList = WebSession.ShopList;
            return View();
        }

        public ActionResult SetShop(int shopid)
        {
            if (shopid > 0)
            {
                WebSession.ShopId = shopid;
                WebSession.ShopName = WebSession.ShopList.FindAll(x => x.ShopId.Equals(shopid))[0].ShopName;
                SetAlertMessage(WebSession.ShopName + " is selected!", Enums.AlertType.success);
            }
            else
            {
                SetAlertMessage("Selected shop is not valid!", Enums.AlertType.danger);
            }

            ViewBag.ShopList = WebSession.ShopList;
            return RedirectToAction("GetShop");
        }

        public ActionResult GetDowntime()
        {
            return View();
        }

        public ActionResult SetDowntime([Bind(Exclude="Id")]DowntimeModel model)
        {
            if (ModelState.IsValid)
            {
                SettingDetails _details = new SettingDetails();
                Enums.CrudStatus status = _details.AddDowntime(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDowntime");
        }

        public ActionResult UpdateDowntime(DowntimeModel model)
        {
            if (ModelState.IsValid)
            {
                SettingDetails _details = new SettingDetails();
                Enums.CrudStatus status = _details.AddDowntime(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDowntime");
        }

        public ActionResult DeleteDowntime(DowntimeModel model)
        {
            if (ModelState.IsValid)
            {
                SettingDetails _details = new SettingDetails();
                Enums.CrudStatus status = _details.AddDowntime(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDowntime");
        }

        public JsonResult GetDowntimeJson()
        {
            SettingDetails _details = new SettingDetails();
            return Json(_details.DowntimeList());
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