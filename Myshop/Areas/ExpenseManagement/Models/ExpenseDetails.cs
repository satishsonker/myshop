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
                         ExpTypeName = x.Gbl_Master_ExpenseType.ExpenseType,
                         UnitId = x.UnitId,
                         UnitName = x.Gbl_Master_Unit.UnitName
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
                         x.TotalAmout,
                         x.IsCancelled
                     }).ToList();
                return expItemList;
            }

            return new List<ExpItemModel>();
        }


        public List<ExpListModel> ExpenseList(DateTime fromdate, DateTime todate, int payModeId = 0, int pageSize = 10, int pageNo = 1)
        {
            myshop = new MyshopDb();
            var _list = myshop.Exp_Tr_New.Where(exp => !exp.IsDeleted && DbFunctions.TruncateTime(exp.CreatedDate) >= fromdate && DbFunctions.TruncateTime(exp.CreatedDate) <= todate && exp.ShopId.Equals(WebSession.ShopId) && (payModeId.Equals(0) || exp.PayModeId == payModeId)).ToList();
            var _returnList = _list.Select(x => new ExpListModel
            {
                CancelledDate = x.CancelledDate ?? DateTime.MinValue,
                CancelReason = x.CancelReason,
                IsCancelled = x.IsCancelled,
                BalanceAmount = x.BalanceAmount,
                BalancePaidAmount = x.BalancePaidAmount,
                BalancePaidDate = x.BalancePaidDate ?? DateTime.MinValue,
                BalPayMode = x.Gbl_Master_PayMode1?.PayMode ?? "No Ref",
                BalPayModeRefNo = x.BalPayModeRefNo,
                IsBalancePaid = x.IsBalancePaid,
                CreatedDate = x.CreatedDate,
                ExpId = x.ExpId,
                PayMode = x.Gbl_Master_PayMode?.PayMode,
                VendorName = x.Gbl_Master_Vendor?.VendorName ?? "No Vendor",
                PaidAmount = x.PaidAmount,
                PayModeRefNo = x.PayModeRefNo ?? "No Ref",
                TotalAmout = x.TotalAmout,
                CreatedBy = x.Exp_Dtl_New.FirstOrDefault().Gbl_Master_User.Firstname + " " + x.Exp_Dtl_New.FirstOrDefault().Gbl_Master_User.Lastname,
                TotalRecords = _list.Count()
            }).OrderBy(x => x.ExpId).ToList().Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            return _returnList;
        }

        public ExpenseJsonModel GetExpenseDetail(int ExpId)
        {
            myshop = new MyshopDb();
            ExpenseJsonModel expense = new ExpenseJsonModel
            {
                ExpTr = myshop.Exp_Tr_New.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId) && x.ExpId.Equals(ExpId))
                .Select(x => new ExpTrModel
                {
                    BalanceAmount = x.BalanceAmount,
                    Date = x.CreatedDate,
                    VendorName = x.Gbl_Master_Vendor.VendorName ?? "No Vendor",
                    VendorId = x.VendorId,
                    ExpTrId = x.ExpId,
                    PaidAmount = x.PaidAmount,
                    PayMode = x.Gbl_Master_PayMode.PayMode,
                    PayModeId = x.PayModeId,
                    PayRefNo = x.PayModeRefNo ?? "",
                    TotalAmount = x.TotalAmout,
                    BalancePaidAmount = x.BalancePaidAmount,
                    BalancePaidDate = x.BalancePaidDate ?? DateTime.MinValue,
                    BalPayMode = x.Gbl_Master_PayMode1.PayMode,
                    BalPayModeRefNo = x.BalPayModeRefNo,
                    IsBalancePaid = x.IsBalancePaid,
                    CancelledDate = x.CancelledDate ?? DateTime.MinValue,
                    CancelReason = x.CancelReason ?? string.Empty,
                    IsCancelled = x.IsCancelled,
                }).FirstOrDefault()
            };
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
                        CancelledDate = x.CancelledDate ?? DateTime.MinValue,
                        IsCancelled = x.IsCancelled,
                        ExpItemPrice = x.Gbl_Master_ExpenseItem.Price,
                        CancelReason = x.CancelReason ?? string.Empty,
                    }).ToList();
            }
            return expense;
        }

        public Enums.CrudStatus CancelExpenseItem(int ExpenseId, int ExpenseDetailId, string CancelReason)
        {
            myshop = new MyshopDb();
            var expenseDetails = myshop.Exp_Dtl_New.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId) && x.ExpTrId.Equals(ExpenseId)).ToList();
            int alreadyCancelled = expenseDetails.Where(x => x.IsCancelled).Count();
            var cancelableItem = expenseDetails.Where(x => x.ExpDtlId.Equals(ExpenseDetailId)).FirstOrDefault();
            if (cancelableItem != null)
            {
                cancelableItem.IsCancelled = true;
                cancelableItem.CancelReason = CancelReason;
                cancelableItem.CancelledDate = DateTime.Now;
                cancelableItem.ModifiedBy = WebSession.UserId;
                cancelableItem.ModifiedDate = DateTime.Now;
                cancelableItem.IsSync = false;
                myshop.Entry(cancelableItem).State = EntityState.Modified;
                int result = myshop.SaveChanges();

                var expTr = myshop.Exp_Tr_New.Where(x => !x.IsDeleted && x.ExpId.Equals(ExpenseId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (expTr != null)
                {
                    if (expenseDetails.Count == alreadyCancelled + result)
                    {
                        expTr.IsCancelled = true;
                        expTr.CancelledDate = DateTime.Now;
                        expTr.CancelReason = "All Items Cancelled";
                    }
                    expTr.BalanceAmount -= cancelableItem.Qty * cancelableItem.Gbl_Master_ExpenseItem.Price;
                    expTr.IsSync = false;
                    expTr.ModifiedBy = WebSession.UserId;
                    expTr.ModifiedDate = DateTime.Now;
                    myshop.SaveChanges();
                }
                return result > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
            }
            return Enums.CrudStatus.NotExist;
        }
        public Enums.CrudStatus CancelExpense(int ExpenseId, string CancelReason)
        {
            myshop = new MyshopDb();
            int result = 0;
            var expTr = myshop.Exp_Tr_New.Where(x => !x.IsDeleted && x.ExpId.Equals(ExpenseId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
            if (expTr != null)
            {
                expTr.IsCancelled = true;
                expTr.IsSync = false;
                expTr.ModifiedBy = WebSession.UserId;
                expTr.ModifiedDate = DateTime.Now;
                expTr.CancelledDate = DateTime.Now;
                expTr.BalanceAmount -= expTr.TotalAmout;
                expTr.CancelReason = "All Items Cancelled";
                result = myshop.SaveChanges();
            }
            return result > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
        }
    }
}