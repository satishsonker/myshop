using DataLayer;
using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class ExpenseDetails
    {
        MyshopDb myshop = null;

        public Tuple<Enums.CrudStatus, int> SetExpense(ExpenseModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();
                int result = 0;

                Exp_Tr_New newexp = new Exp_Tr_New
                {
                    BalanceAmount = model.ExpTr.BalanceAmount,
                    CreatedBy = WebSession.UserId,
                    CreatedDate = DateTime.Now,
                    PaidAmount = model.ExpTr.PaidAmount,
                    IsDeleted = false,
                    IsSync = false,
                    ShopId = WebSession.ShopId,
                    PayModeId = model.ExpTr.PayModeId,
                    PayModeRefNo = model.ExpTr.PayModeRefNo,
                    TotalAmout = model.ExpTr.TotalAmout,
                    VendorId = model.ExpTr.VendorId
                };
                myshop.Entry(newexp).State = EntityState.Added;
                result = myshop.SaveChanges();
                if (result > 0)
                {
                    foreach (var dtl in model.ExpDtl)
                    {
                        Exp_Dtl_New newExpDtl = new Exp_Dtl_New
                        {
                            CreatedBy = WebSession.UserId,
                            CreatedDate = DateTime.Now,
                            ExpItemId = dtl.ExpItemId,
                            ExpTrId = newexp.ExpId,
                            IsDeleted = false,
                            IsSync = false,
                            Qty = dtl.Qty,
                            ShopId = WebSession.ShopId
                        };
                        myshop.Exp_Dtl_New.Add(newExpDtl);
                    }
                }

                result = myshop.SaveChanges();
                return new Tuple<Enums.CrudStatus, int>(Utility.CrudStatus(result, crudType), newexp.ExpId);
            }
            catch (Exception ex)
            {
                return new Tuple<Enums.CrudStatus, int>(Enums.CrudStatus.Exception, 0);
            }
            finally
            {

            }
        }
        public List<ExpItemModel> SearchExpItem(string searchValue = "")
        {
            if (searchValue != string.Empty)
            {
                myshop = new MyshopDb();
                var expItemList = myshop.Gbl_Master_ExpenseItem.Where(x => x.Name.ToLower().IndexOf(searchValue.ToLower().Trim()) > -1 && !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId))
                     .Select(x => new ExpItemModel
                     {
                         CreatedDate = x.CreatedDate,
                         ExpItem = x.Name,
                         ExpItemDesc = x.Description,
                         ExpItemId = x.Id,
                         ExpItemPrice = x.Price,
                         ExpTypeId = x.ExpTypeId,
                         ExpTypeName = x.Gbl_Master_ExpenseType.ExpenseType
                     }).ToList();
                return expItemList;
            }

            return new List<ExpItemModel>();
        }
        public IEnumerable<object> SearchExpNo(string searchValue = "")
        {
            if (searchValue != string.Empty)
            {
                myshop = new MyshopDb();
                var expItemList = myshop.Exp_Tr_New.Where(x => x.ExpId.ToString().IndexOf(searchValue.Trim()) > -1 && !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId))
                     .Select(x => new
                     {
                         x.ExpId,
                         x.Gbl_Master_Vendor.VendorName,
                         x.TotalAmout
                     }).ToList();
                return expItemList;
            }

            return new List<ExpItemModel>();
        }


        public List<ExpListModel> ExpenseList(DateTime fromdate, DateTime todate,int payModeId=0,int pageSize=10,int pageNo=1)
        {
            myshop = new MyshopDb();
            var _list = myshop.Exp_Tr_New.Where(exp => !exp.IsDeleted && exp.CreatedDate >= fromdate && exp.CreatedDate <= todate && exp.ShopId.Equals(WebSession.ShopId) && (payModeId.Equals(0) || exp.PayModeId == payModeId)).ToList();
            var _returnList= _list.Select(x => new ExpListModel
            {
                BalanceAmount = x.BalanceAmount ?? 0.00M,
                CreatedDate = x.CreatedDate,
                ExpId = x.ExpId,
                PayMode = x.Gbl_Master_PayMode?.PayMode,
                VendorName = x.Gbl_Master_Vendor?.VendorName??"No Vendor",
                PaidAmount = x.PaidAmount,
                PayModeRefNo = x.PayModeRefNo ?? "No Ref",
                TotalAmout = x.TotalAmout,
                TotalRecords = _list.Count()
                }).OrderBy(x=>x.ExpId).ToList().Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
            return _returnList;
        }

        public ExpenseJsonModel GetExpenseDetail(int ExpId)
        {
            myshop = new MyshopDb();
            ExpenseJsonModel expense = new ExpenseJsonModel();
            expense.ExpTr = myshop.Exp_Tr_New.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId) && x.ExpId.Equals(ExpId))
                .Select(x => new ExpTrModel
                {
                    BalanceAmount = x.BalanceAmount ?? 0.00M,
                    Date = x.CreatedDate,
                    VendorName = x.Gbl_Master_Vendor.VendorName ?? "No Vendor",
                    VendorId = x.VendorId ?? 0,
                    ExpTrId = x.ExpId,
                    PaidAmount = x.PaidAmount,
                    PayMode = x.Gbl_Master_PayMode.PayMode,
                    PayModeId = x.PayModeId,
                    PayRefNo = x.PayModeRefNo ?? "",
                    TotalAmount = x.TotalAmout
                }).FirstOrDefault();
            if (expense.ExpTr != null)
            {
                expense.ExpDtl = myshop.Exp_Dtl_New.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId) && x.ExpTrId.Equals(ExpId))
                    .Select(x => new ExpDtlModel
                    {
                        ExpDtlId = x.ExpDtlId,
                        ExpItemId = x.ExpItemId,
                        ExpItemName = x.Gbl_Master_ExpenseItem.Name,
                        ExpTypeId = x.Gbl_Master_ExpenseItem.Gbl_Master_ExpenseType.Id,
                        ExpTypeName = x.Gbl_Master_ExpenseItem.Gbl_Master_ExpenseType.ExpenseType,
                        Qty = x.Qty,
                        ExpItemPrice= x.Gbl_Master_ExpenseItem.Price
                    }).ToList();
            }
            return expense;
        }
    }
}