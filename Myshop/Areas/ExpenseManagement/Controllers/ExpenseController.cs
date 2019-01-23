using Myshop.App_Start;
using Myshop.Areas.ExpenseManagement.Models;
using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Linq;
using System.Web.Mvc;
using static Myshop.App_Start.Enums;

namespace Myshop.Areas.ExpenseManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class ExpenseController : CommonController
    {
        // GET: ExpenseManagement/Expense
        [HttpGet]
        public ActionResult AddExpenses()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ExpenseList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ExpenseDetails()
        {
            return View();
        }

        public JsonResult SaveExpense(ExpenseModel model)
        {
            ExpenseDetails _details = new ExpenseDetails();
            Tuple<Enums.CrudStatus, int> result=_details.SetExpense(model, CrudType.Insert);
            return Json(ReturnAjaxAlertMessage(result.Item1, result.Item2).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchExpenseItem(string SearchValue)
        {
            ExpenseDetails _details = new ExpenseDetails();
            return Json(_details.SearchExpItem(SearchValue), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchExpenseNo(string SearchValue)
        {
            ExpenseDetails _details = new ExpenseDetails();
            return Json(_details.SearchExpNo(SearchValue), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExpenseList(DateTime from,DateTime to,int payModeId = 0, int pageSize = 10, int pageNo = 1)
        {
            ExpenseDetails _details = new ExpenseDetails();
            return Json(_details.ExpenseList(from,to,payModeId,pageSize,pageNo), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExpenseDetails(int ExpId)
        {
            ExpenseDetails _details = new ExpenseDetails();
            return Json(_details.GetExpenseDetail(ExpId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelExpenseItem(int ExpId,int ExpDtlId,string CancelReason)
        {
            ExpenseDetails _details = new ExpenseDetails();
            return Json(ReturnAjaxAlertMessage(_details.CancelExpenseItem(ExpId,ExpDtlId,CancelReason)).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CancelExpense(int ExpId, string CancelReason)
        {
            ExpenseDetails _details = new ExpenseDetails();
            return Json(ReturnAjaxAlertMessage(_details.CancelExpense(ExpId,CancelReason)).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}