using Myshop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Myshop.Areas.EmployeesManagement.Models;
using Myshop.App_Start;
using Myshop.Controllers;

namespace Myshop.Areas.EmployeesManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class MasterController : CommonController
    {

        public ActionResult AddEmployee()
        {
            Gbl_Master_Employee model = new Gbl_Master_Employee();
            return View(model);
        }

        public ActionResult AddEmployeeRole()
        {
            return View();
        }

        public ActionResult SetRoleType(Gbl_Master_Employee_Role model)
        {           
            if (ModelState.IsValid)
            {
                MastersDetails details = new MastersDetails();
                Enums.CrudStatus status = details.SetEmployeeRole(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            
            return View("AddEmployeeRole");
        }

        public ActionResult SetEmployee(Gbl_Master_Employee model)
        {
           HttpPostedFileBase Files = Request.Files[0];
            if (ModelState.IsValid)
            {
                Enums.FileValidateStatus fileStatus = ValidateFiles(Request.Files, Enums.FileType.Image,1024,1);
                MastersDetails details = new MastersDetails();
                Tuple<Enums.CrudStatus, Gbl_Master_Employee> status = details.SetEmployee(model, Enums.CrudType.Insert,Files);
                ReturnAlertMessage(status.Item1);
            }

            return View("AddEmployee",model);
        }
        public ActionResult UpdateRoleType(Gbl_Master_Employee_Role model)
        {
            if (ModelState.IsValid)
            {
                MastersDetails details = new MastersDetails();
                Enums.CrudStatus status = details.SetEmployeeRole(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return View("AddEmployeeRole");
        }
        public ActionResult DeleteRoleType(Gbl_Master_Employee_Role model)
        {
            if (ModelState.IsValid)
            {
                MastersDetails details = new MastersDetails();
                Enums.CrudStatus status = details.SetEmployeeRole(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return View("AddEmployeeRole");
        }
        public ActionResult UpdateEmployee(Gbl_Master_Employee model,HttpPostedFileBase Files)
        {
            if (ModelState.IsValid)
            {
                MastersDetails details = new MastersDetails();
                Tuple<Enums.CrudStatus, Gbl_Master_Employee> status = details.SetEmployee(model, Enums.CrudType.Update,Files);
                ReturnAlertMessage(status.Item1);
            }
            return View("AddEmployee",model);
        }
        public ActionResult DeleteEmployee(Gbl_Master_Employee model)
        {
            if (ModelState.IsValid)
            {
                MastersDetails details = new MastersDetails();
                Tuple<Enums.CrudStatus, Gbl_Master_Employee> status = details.SetEmployee(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status.Item1);
            }
            return View("AddEmployee", model);
        }
        public JsonResult GetEmpRoleJson()
        {
           return Json(GlobalMethod.GetEmpRole());
        }

        public JsonResult GetEmpJson()
        {
            return Json(GlobalMethod.GetDdEmpList());
        }

        public ActionResult GetRoleTypeJson()
        {
            if (ModelState.IsValid)
            {
                MastersDetails details = new MastersDetails();
                return Json(details.GetRoleTypeJson());
            }
            return View("AddEmployeeRole");
        }

        public JsonResult GetEmpListJson(int PageNo=1,int PageSize=10)
        {
                MastersDetails details = new MastersDetails();
                return Json(details.GetEmpJson(PageNo,PageSize));
        }
    }
}