using Myshop.App_Start;
using Myshop.Areas.StockManagement.Models;
using Myshop.Controllers;
using Myshop.Filters;
using System.Web.Mvc;
using DataLayer;
using System.Collections.Generic;

namespace Myshop.Areas.StockManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class StockController : CommonController
    {
        MastersModel model = null;
        StockEntryDetails details = null;
        public ActionResult Main()
        {
            return View();
        }

        [RestoreModelState]
        public ActionResult StockEntry()
        {
            return View();
        }

        [SetModelState]
        public ActionResult SetStockEntry(StockEntryModel model,List<Stk_Dtl_Entry> list)
        {

            if (ModelState.IsValid)
            {
                details = new StockEntryDetails();
                Enums.CrudStatus status = details.SetStock(model,list, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("StockEntry");
        }

        [SetModelState]
        public ActionResult UpdateStockEntry(StockEntryModel model, List<Stk_Dtl_Entry> list)
        {
            if (ModelState.IsValid)
            {
                details = new StockEntryDetails();
                Enums.CrudStatus status = details.SetStock(model, list, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("StockEntry");
        }

        [SetModelState]
        public ActionResult DeleteStockEntry(StockEntryModel model, List<Stk_Dtl_Entry> list)
        {
            if (ModelState.IsValid)
            {
                details = new StockEntryDetails();
                Enums.CrudStatus status = details.SetStock(model, list, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("StockEntry");
        }

        [HttpPost]
        public JsonResult GetStockJosn()
        {
            details = new StockEntryDetails();
            return Json(details.GetStockJson(),JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetUniqueStockProducts()
        {
            details = new StockEntryDetails();
            return Json(details.GetUniqueStockProducts(), JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult GetStockDetailsJosn(int stockId)
        {
            details = new StockEntryDetails();
            return Json(details.GetStockDetailsJson(stockId),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStockEntryProductChartData(int[] ProductId,int Duration=30)
        {
            DashboardDetail _details = new DashboardDetail();
            return Json(_details.GetStockEntryChartData(ProductId, Duration), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetStockEntryTotalAmountChartData(int[] ProductId, int Duration = 30)
        {
            DashboardDetail _details = new DashboardDetail();
            return Json(_details.GetStockEntryTotalAmountChartData(ProductId, Duration), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStockEntryTotalQuantityChartData(int[] ProductId, int Duration = 30)
        {
            DashboardDetail _details = new DashboardDetail();
            return Json(_details.GetStockEntryTotalQuantityChartData(ProductId, Duration), JsonRequestBehavior.AllowGet);
        }
    }
}