﻿using Myshop.App_Start;
using Myshop.Areas.Global.Models;
using System;
using Myshop.Filters;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Myshop.GlobalResource;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class UserController : Controller
    {
        // GET: Global/User
        public ActionResult GetUser()
        {
            return View();
        }
        public ActionResult SetUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                UserDetails _details = new UserDetails();
                if (WebSession.UserType.ToLower().Contains("super") && WebSession.UserType.ToLower().Contains("admin"))
                {
                    Enums.CrudStatus status = _details.SetUser(model, Enums.CrudType.Insert);
                    ReturnAlertMessagToView(status);
                }
                else
                    SetAlertMessage("You do not have rights to create user", Enums.AlertType.warning);
            }
            return View("GetUser");
        }
        public ActionResult UpdateUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                UserDetails _details = new UserDetails();
                if (WebSession.UserType.ToLower().Contains("super") && WebSession.UserType.ToLower().Contains("admin"))
                {
                    Enums.CrudStatus status = _details.SetUser(model, Enums.CrudType.Update);
                    ReturnAlertMessagToView(status);
                }
                else
                    SetAlertMessage("You do not have rights to update user", Enums.AlertType.warning);
            }
            return View("GetUser");
        }
        public ActionResult DeleteUser(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                UserDetails _details = new UserDetails();
                if (WebSession.UserType.ToLower().Contains("super") && WebSession.UserType.ToLower().Contains("admin"))
                {
                    Enums.CrudStatus status = _details.SetUser(model, Enums.CrudType.Delete);
                    ReturnAlertMessagToView(status);
                }
                else
                    SetAlertMessage("You do not have rights to delete user", Enums.AlertType.warning);
            }
            return View("GetUser");
        }
        public JsonResult GetUserJson(bool allList=false)
        {
            try
            {
                UserDetails model = new UserDetails();
                return Json(model.GetUserJson(allList));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        public ActionResult GetPermission()
        {
            return View();
        }
        public JsonResult GetPermissionJson(int userid,int moduleId)
        {
            try
            {
                if (userid > 0)
                {
                    UserDetails model = new UserDetails();
                    return Json(model.GetPermissionJson(userid,moduleId));
                }
                else
                    return Json("Invalid User Id");
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult UpdateSinglePermission(List<PermissionModel> model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDetails details = new UserDetails();
                    return Json(ReturnAlertMessage(details.UpdateSinglePermission(model,Enums.CrudType.Update)));
                }
                else
                    return Json(GetErrorList());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult SaveSinglePermission(List<PermissionModel> model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDetails details = new UserDetails();
                    return Json(ReturnAlertMessage(details.UpdateSinglePermission(model, Enums.CrudType.Insert)));
                }
                else
                    return Json(GetErrorList());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult DeletesSinglePermission(List<PermissionModel> model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDetails details = new UserDetails();
                    return Json(ReturnAlertMessage(details.UpdateSinglePermission(model, Enums.CrudType.Delete)));
                }
                else
                    return Json("Invalid User Id");
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        public JsonResult GetUserType()
        {
            try
            {
                    UserDetails model = new UserDetails();
                    return Json(model.GetUserTypes());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        public JsonResult isUserExist(string username)
        {
            try
            {               
                return Json(Utility.IsUserExist(username));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        public ActionResult MapShop()
        {
            return View();
        }

        public JsonResult GetShopJson()
        {
            try
            {
                    UserDetails details = new UserDetails();
                    return Json(details.GetShop());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetShopMap(int userId)
        {
            try
            {
                UserDetails details = new UserDetails();
                return Json(details.GetMapJson(userId));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult SetShopJson(int userid,int shopid)
        {
            try
            {
                UserDetails details = new UserDetails();
                return Json(ReturnAlertMessage(details.SetShopMap(userid, shopid, Enums.CrudType.Insert)));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult DeleteShopMap(int userid,int shopid)
        {
            try
            {
                UserDetails details = new UserDetails();
                return Json(ReturnAlertMessage(details.SetShopMap(userid, shopid, Enums.CrudType.Delete)));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        public ActionResult GetAccess()
        {
            return View();
        }

        public JsonResult UpdateUserAccess(List<UserAccessModel> list)
        {
            if(ModelState.IsValid)
            {
                UserDetails details=new UserDetails();
                return Json(ReturnAlertMessage(details.SetAccess(list)));
            }
            return Json(ModelState.Values);
        }
        private void SetAlertMessage(string message, Enums.AlertType alert)
        {
            ViewBag.message = message;
            TempData["messages"] = message;
            ViewBag.alert = alert.ToString();
            TempData["alert"] = alert.ToString();
        }

        private string ReturnAlertMessage(Enums.CrudStatus status)
        {
            if (status == Enums.CrudStatus.Deleted)
            {
                return Resource.DataDeleted;
            }
            else if (status == Enums.CrudStatus.Inserted)
            {
                return Resource.DataSaved;
            }
            else if (status == Enums.CrudStatus.Updated)
            {
                return Resource.DataUpdated;
            }
            else if (status == Enums.CrudStatus.AlreadyExistForSameShop)
            {
                return Resource.DataExistWithSameShopName;
            }
            else if (status == Enums.CrudStatus.NoEffect)
            {
                return Resource.DataNotSaved;
            }
            else if (status == Enums.CrudStatus.Exception)
            {
                return Resource.Exception;
            }
            else if (status == Enums.CrudStatus.AlreadyInUse)
            {
                return Resource.AlreadyInUse;
            }
            else
                return "Nothing happend";
        }

        private void ReturnAlertMessagToView(Enums.CrudStatus status)
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

        private IEnumerable<string> GetErrorList()
        {
           return ViewData.ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage.ToString()));
        }

    }
}