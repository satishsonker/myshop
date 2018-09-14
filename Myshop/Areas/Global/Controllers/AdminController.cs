using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Areas.Global.Models;
using Myshop.Filters;
using Myshop.Controllers;
using Myshop.App_Start;
using DataLayer;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class AdminController : CommonController
    {
        AdminDetails _details = null;
        // GET: Global/Admin
        public ActionResult GetErrorLog(bool isAllLog=false)
        {
            _details = new AdminDetails();
            return View(_details.GetErrorLog(isAllLog));
        }

        [HttpGet]
        public ActionResult ResetUserPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateUserTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUserTask(Gbl_Master_Task _model)
        {
            _details = new AdminDetails();
            ReturnAlertMessage(_details.TaskCreate(Enums.CrudType.Insert, _model));
            return View("CreateUserTask");
        }

        [HttpPost]
        public ActionResult UpdateUserTask(Gbl_Master_Task _model)
        {
            _details = new AdminDetails();
            ReturnAlertMessage(_details.TaskCreate(Enums.CrudType.Update, _model));
            return View("CreateUserTask");
        }

        [HttpPost]
        public ActionResult DeleteUserTask(Gbl_Master_Task _model)
        {
            _details = new AdminDetails();
            ReturnAlertMessage(_details.TaskCreate(Enums.CrudType.Delete, _model));
            return View("CreateUserTask");
        }

        [HttpPost]
        public JsonResult TaskMarkComplete(int taskId)
        {
            _details = new AdminDetails();
            return Json(ReturnAjaxAlertMessage(_details.TaskMarkComplete(taskId)).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TaskUserList(bool allUserList=false,bool addCompleteList=false)
        {
            _details = new AdminDetails();
           return Json(_details.TaskUserList(allUserList, addCompleteList),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ResetUserPassword(int _userId)
        {
            _details = new AdminDetails();
            ReturnAlertMessage(_details.ResetUserPassword(_userId));
            return View("ResetUserPassword");
        }

        public JsonResult UpdateErrorLog(int ErrorId)
        {
            _details = new AdminDetails();
            return Json(_details.UpdateErrorLog(ErrorId));
        }
    }
}