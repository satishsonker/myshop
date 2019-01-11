using Myshop.Areas.ExpenseManagement.Models;
using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Myshop.App_Start.Enums;

namespace Myshop.Areas.ExpenseManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class MasterController : CommonController
    {
        // GET: ExpenseManagement/Master
        [HttpGet]
        public ActionResult AddExpenseType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetExpenseType(ExpTypeModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails masterDetails = new MasterDetails();
                ReturnAlertMessage(masterDetails.SetExpType(model, CrudType.Insert));
                
            }
            return RedirectToAction("AddExpenseType");
        }
        [HttpPost]
        public ActionResult UpdateExpenseType(ExpTypeModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails masterDetails = new MasterDetails();
                ReturnAlertMessage(masterDetails.SetExpType(model, CrudType.Update));

            }
            return RedirectToAction("AddExpenseType");
        }
        [HttpPost]
        public ActionResult DeleteExpenseType(ExpTypeModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails masterDetails = new MasterDetails();
                ReturnAlertMessage(masterDetails.SetExpType(model, CrudType.Delete));

            }
            return RedirectToAction("AddExpenseType");
        }

        [HttpGet]
        public ActionResult AddExpenseItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetExpenseItem(ExpItemModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails masterDetails = new MasterDetails();
                ReturnAlertMessage(masterDetails.SetExpItem(model, CrudType.Insert));

            }
            return RedirectToAction("AddExpenseItem");
        }
        [HttpPost]
        public ActionResult UpdateExpenseItem(ExpItemModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails masterDetails = new MasterDetails();
                ReturnAlertMessage(masterDetails.SetExpItem(model, CrudType.Update));

            }
            return RedirectToAction("AddExpenseItem");
        }
        [HttpPost]
        public ActionResult DeleteExpenseItem(ExpItemModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails masterDetails = new MasterDetails();
                ReturnAlertMessage(masterDetails.SetExpItem(model, CrudType.Delete));

            }
            return RedirectToAction("AddExpenseItem");
        }

        public JsonResult GetExpTypeJson()
        {
                MasterDetails masterDetails = new MasterDetails();
                return Json(masterDetails.GetExpTypeJson(),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetExpItemJson()
        {
            MasterDetails masterDetails = new MasterDetails();
            return Json(masterDetails.GetExpItemJson(), JsonRequestBehavior.AllowGet);
        }
    }
}