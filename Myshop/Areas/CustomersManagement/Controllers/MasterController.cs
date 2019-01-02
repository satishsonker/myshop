using Myshop.App_Start;
using System.Web.Mvc;
using Myshop.Areas.CustomersManagement.Models;
using System;
using Myshop.Controllers;

namespace Myshop.Areas.CustomersManagement.Controllers
{
    public class MasterController : CommonController
    {
        MasterModel _details = null;

        // GET: CustomersManagement/Master
        public ActionResult AddCustmerType()
        {
            return View();
        }

        public ActionResult AddCustmer()
        {
            return View();
        }

        public ActionResult SetCustmerType(CustomerTypeModel model)
        {
            if (ModelState.IsValid)
            {
                _details = new MasterModel();
                Enums.CrudStatus status = _details.SetCustomerType(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return View("AddCustmerType");
        }
        public ActionResult UpdateCustmerType(CustomerTypeModel model)
        {
            if (ModelState.IsValid)
            {
                _details = new MasterModel();
                Enums.CrudStatus status = _details.SetCustomerType(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return View("AddCustmerType");
        }
        public ActionResult DeleteCustmerType(CustomerTypeModel model)
        {
            if (ModelState.IsValid)
            {
                _details = new MasterModel();
                Enums.CrudStatus status = _details.SetCustomerType(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return View("AddCustmerType");
        }

        public ActionResult SetCustmer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _details = new MasterModel();
                Enums.CrudStatus status = _details.SetCustomer(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return View("AddCustmer");
        }

        public ActionResult UpdateCustmer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _details = new MasterModel();
                Enums.CrudStatus status = _details.SetCustomer(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return View("AddCustmer");
        }

        public ActionResult DeleteCustmer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _details = new MasterModel();
                Enums.CrudStatus status = _details.SetCustomer(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return View("AddCustmer");
        }

        public JsonResult GetCustmerTypeJson()
        {
            try
            {
                _details = new MasterModel();
                return Json(_details.GetCustomerTypeJson(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetCustomerTypeSelectList()
        {
            try
            {
                return Json(GlobalMethod.GetCustomerType(),JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Invalid Error");
            }
        }

        public JsonResult GetCustmerJson(string mobile="")
        {
            try
            {
                _details = new MasterModel();
                return Json(_details.GetCustomerJson(mobile), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Invalid Error");
            }
        }
    }
}