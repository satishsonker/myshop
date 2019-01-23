using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class ReportsDetails
    {
        MyshopDb myshop = null;
        public Dictionary<string,List<BalanceModel>> GetBalanceReport(int Year=0,int Month=0,int VendorId=0)
        {
            myshop = new MyshopDb();
            Dictionary<string, List<BalanceModel>> returnData = new Dictionary<string, List<BalanceModel>>();
            var data = myshop.Exp_Tr_New.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)
             && x.BalanceAmount!=0 && (Year.Equals(0) || x.CreatedDate.Year.Equals(Year)) && (Month.Equals(0) || x.CreatedDate.Month.Equals(Month)) && (VendorId.Equals(0) || x.VendorId.Equals(VendorId))).GroupBy(x=>x.VendorId).ToList();
            foreach (var item in data)
            {
                List<BalanceModel> list = item.Select(x => new BalanceModel
                {
                    BalanceAmount = x.BalanceAmount,
                    CreatedDate = x.CreatedDate,
                    VendorName = x.Gbl_Master_Vendor?.VendorName,
                    ExpId = x.ExpId,
                    Data = x.Exp_Dtl_New.Select(y => new BalanceDataModel {
                        Amount=y.Qty*y.Gbl_Master_ExpenseItem.Price,
                        ItemId=y.ExpItemId,
                        ItemName=y.Gbl_Master_ExpenseItem.Name,
                        Price= y.Gbl_Master_ExpenseItem.Price,
                        Qty=y.Qty,
                        Unit=y.Gbl_Master_ExpenseItem.Gbl_Master_Unit.UnitName,
                        CancelDate=y.CancelledDate??DateTime.MinValue,
                        CancelReason=y.CancelReason??string.Empty,
                        IsCancelled=y.IsCancelled
                    }).ToList(),
                    PaidAmount = x.PaidAmount,
                    TotalAmount=x.TotalAmout,
                    VendorId = x.VendorId,
                    CancelDate = x.CancelledDate ?? DateTime.MinValue,
                    CancelReason = x.CancelReason ?? string.Empty,
                    IsCancelled = x.IsCancelled
                }).ToList();


                returnData.Add(item.Select(x => x.Gbl_Master_Vendor.VendorName).FirstOrDefault().ToString(), list);
            }

            return returnData;
        }
    }
}